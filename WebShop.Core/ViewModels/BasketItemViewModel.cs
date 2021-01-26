using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Models;

namespace WebShop.Core.ViewModels
{
    public class BasketItemViewModel
    {
        public IEnumerable<BasketItemDetail> BasketItemDetail { get; set; }
        public DiscountItem DiscountItem { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
    }

    public class DeliveryMethod
    {
        public string  Delivery { get; set; }
        public decimal Price { get; set; }
        public DeliveryMethod()
        {
            this.Delivery = "Store";
            this.Price = 0;
        }
    }

    public class DiscountItem
    {
        public string CouponName { get; set; }
        public decimal Price { get; set; }
    }

    public class BasketItemDetail
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string ProdctName { get; set; }
        public string ProdctId { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
