using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Core.Models;
using WebShop.Core.ViewModels;
using WebShop.Core.Contracts;

namespace WebShop.WebUI.Controllers
{
    //todo redirect to login when not logged in automatically by identity
    //[Authorize]
    public class CheckoutController : Controller
    {
        IRepository<Order> context;
        IBasketService basketService;
        public CheckoutController(IRepository<Order> context, IBasketService basketService)
        {
            this.context = context;
            this.basketService = basketService;
        }

        // GET: CheckOut
        public ActionResult Index()
        {
            /* fill the data from previous order if any */
            String userName = User.Identity.Name;

            //todo below line should be removed after auhtentication
            userName = "Anonymous";
            var previousOrder = context.Collection()
                .Where(o => o.Username == userName)
                .FirstOrDefault();

            var Order = new Order();
            if (previousOrder != null)
            {
                Order.FirstName = previousOrder.FirstName;
                Order.LastName = previousOrder.LastName;
                Order.Address = previousOrder.Address;
                Order.City = previousOrder.City;
                Order.Email = previousOrder.Email;
                Order.Phone = previousOrder.Phone;
                Order.PostalCode = previousOrder.PostalCode;
            }

            return View(Order);
        }


        [HttpPost]
        public ActionResult CheckoutForUser(Order order)
        {
            var Basket = basketService.GetBasket(this.HttpContext, false);
            if(Basket!=null)
            {
                order.Username = String.IsNullOrEmpty(User.Identity.Name)?"Anonymous": User.Identity.Name;
                order.OrderDate = DateTime.Now;
                order.BasketId = Basket.Id;
                context.Insert(order);
                context.Commit();
            }
            //todo need to route to my pages
            basketService.clearCookie(this.HttpContext);
            return RedirectToAction("Index", "Checkout");

        }
    }
}