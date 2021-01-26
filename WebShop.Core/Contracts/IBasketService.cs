using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.ViewModels;
using System.Web;

namespace WebShop.Core.Contracts
{
    public interface IBasketService
    {
        void AddToBasket(HttpContextBase httpContext, string productId,int qty);
        void RemoveFromBasket(HttpContextBase httpContext, string itemId);
        void ReduceQuantity(HttpContextBase httpContext, string itemId);
        void IncreaseQuantity(HttpContextBase httpContext, string itemId);
        void AddCoupon(HttpContextBase httpContext, string coupon);
        void SetDelivery(HttpContextBase httpContext, string Delivery);
        BasketItemViewModel GetBasketItems(HttpContextBase httpContext);
        BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext);
    }
}
