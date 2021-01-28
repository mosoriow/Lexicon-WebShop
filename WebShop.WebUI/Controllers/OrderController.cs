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
    // redirect to login when not logged in automatically by identity
    [Authorize]
    public class OrderController : Controller
    {
        IRepository<Order> context;
        IBasketService basketService;

        public OrderController(IRepository<Order> context, IBasketService basketService)
        {
            this.context = context;
            this.basketService = basketService;
        }

        //get the username, get the orders, get the basket and show in a table (partial view for basket)
        public ActionResult MyOrders()
        {
            String userName = User.Identity.Name;
            var myOrders = context.Collection()
                .Where(o => o.Username == userName)
                .OrderByDescending(o=>o.CreateAt)
               .ToList();
            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();
            foreach (var order in myOrders)
            {
                var BasketItemViewModel = basketService.GetBasketItems(order.BasketId);
                orderViewModels.Add(new OrderViewModel(order, BasketItemViewModel));
            }
            return View(orderViewModels);
        }
    }
}