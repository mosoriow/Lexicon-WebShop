using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Core.Contracts;
using WebShop.Core.ViewModels;

namespace WebShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IBasketService basketService;
        public BasketController(IBasketService BasketService)
        {
            this.basketService = BasketService;
        }

        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddToBasket(string productid, int qty)
        {
            basketService.AddToBasket(this.HttpContext, productid, qty);

            return RedirectToAction("Index");
        }

        public ActionResult AddToBasket(string productid)
        {
            basketService.AddToBasket(this.HttpContext, productid, 1);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public ActionResult ReduceQuantity(string Id)
        {
            basketService.ReduceQuantity(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public ActionResult IncreaseQuantity(string Id)
        {
            basketService.IncreaseQuantity(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public ActionResult AddCoupon(string coupon)
        {
            basketService.AddCoupon(this.HttpContext, coupon);

            return RedirectToAction("Index");
        }

        public ActionResult SetDelivery(string Delivery)
        {
            basketService.SetDelivery(this.HttpContext, Delivery);

            return RedirectToAction("Index");
        }
        
        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);

            return PartialView(basketSummary);
        }

        public PartialViewResult BasketItemTable(BasketItemViewModel model)
        {
            if(model==null|| model.BasketItemDetail==null || model.BasketItemDetail.Count() == 0)
                model = basketService.GetBasketItems(this.HttpContext);
            return PartialView(model);
        }

    }
}