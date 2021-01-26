using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Contracts;
using WebShop.Core.Models;
using WebShop.Core.ViewModels;
using System.Web;

namespace WebShop.Services
{
    public class BasketService : IBasketService
    {
        IRepository<Product> productContext;
        IRepository<Basket> basketContext;

        public const string BasketSessionName = "eCommerceBasket";

        public BasketService(IRepository<Product> ProductContext, IRepository<Basket> BasketContext)
        {
            this.basketContext = BasketContext;
            this.productContext = ProductContext;
        }

        public Basket GetBasket(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);

            Basket basket = new Basket();

            if (cookie != null)
            {
                string basketId = cookie.Value;
                if (!string.IsNullOrEmpty(basketId))
                {
                    basket = basketContext.Find(basketId);
                }
                else
                {
                    if (createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }
            }

            return basket;

        }

        public void clearCookie(HttpContextBase httpContext)
        {
            httpContext.Response.Cookies.Set(new HttpCookie(BasketSessionName) { Value = string.Empty });
        }

        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketContext.Insert(basket);
            basketContext.Commit();

            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }

        public void AddToBasket(HttpContextBase httpContext, string productId, int qty)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);
            if (qty <= 0 || qty > 5)
            {
                qty = 1;
            }
                if (item == null)
            {
                    item = new BasketItem()
                    {
                        BasketId = basket.Id,
                        ProductId = productId,
                        Quantity =  qty
                };

                basket.BasketItems.Add(item);
            }
            else
            {
                item.Quantity = item.Quantity + qty;
            }

            basketContext.Commit();
        }

        public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
        }

        public void ReduceQuantity(HttpContextBase httpContext, string itemId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                if(item.Quantity <= 1)
                    basket.BasketItems.Remove(item);
                else
                    item.Quantity = item.Quantity - 1;
                basketContext.Commit();
            }
        }

        public void IncreaseQuantity(HttpContextBase httpContext, string itemId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                    item.Quantity = item.Quantity + 1;
                basketContext.Commit();
            }
        }

        public void AddCoupon(HttpContextBase httpContext, string coupon)
        {
            Basket basket = GetBasket(httpContext, true);
            if (coupon == "FREE100" || coupon == "FREE200")
            {
                basket.CouponName = coupon;
            }
            else
            {
                basket.CouponName = null;
            }
            basketContext.Update(basket);
            basketContext.Commit();
        }

        public void SetDelivery(HttpContextBase httpContext, string Delivery)
        {
                Basket basket = GetBasket(httpContext, true);
                basket.Delivery = Delivery;
            basketContext.Update(basket);
            basketContext.Commit();
        }
        


        public BasketItemViewModel GetBasketItems(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            BasketItemViewModel basketItemViewModel = new BasketItemViewModel();

            if (basket != null)
            {
                var results = (from b in basket.BasketItems
                               join p in productContext.Include(p=>p.Images) on b.ProductId equals p.Id
                               select new BasketItemDetail()
                               {
                                   Id = b.Id,
                                   Quantity = b.Quantity,
                                   ProdctName = p.Name,
                                   ProdctId = p.Id,
                                   Image = p.Images.FirstOrDefault().Path,
                                   Price = p.Price
                               }
                              ).ToList();

                basketItemViewModel.BasketItemDetail = results;

                if(basket.CouponName != null)
                {
                    basketItemViewModel.DiscountItem = new DiscountItem();
                    basketItemViewModel.DiscountItem.CouponName = basket.CouponName;
                    basketItemViewModel.DiscountItem.Price = basket.CouponName=="FREE100"? 100: 200;
                }

                basketItemViewModel.DeliveryMethod = new DeliveryMethod();
                basketItemViewModel.DeliveryMethod.Delivery = basket.Delivery;

                if (basket.Delivery == "Home")
                {
                    basketItemViewModel.DeliveryMethod.Price += 30;
                }
                else if (basket.Delivery == "Express")
                {
                    basketItemViewModel.DeliveryMethod.Price += 50;
                }
            }
            return basketItemViewModel;
        }

        public BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            BasketSummaryViewModel model = new BasketSummaryViewModel(0, 0);
            if (basket != null)
            {
                int? basketCount = (from item in basket.BasketItems
                                    select item.Quantity).Sum();

                decimal? basketTotal = (from item in basket.BasketItems
                                        join p in productContext.Collection() on item.ProductId equals p.Id
                                        select item.Quantity * p.Price).Sum();

                model.BasketCount = basketCount ?? 0;
                model.BasketTotal = basketTotal ?? decimal.Zero;

                if ( basket.CouponName != null)
                {
                    model.BasketTotal -=  basket.CouponName == "FREE100" ? 100 : 200;
                }

                if (basket.Delivery == "Home") model.BasketTotal += 30;
                else if (basket.Delivery == "Express") model.BasketTotal += 50;
                
                    return model;
            }
            else
            {
                return model;
            }
        }
    }
}
