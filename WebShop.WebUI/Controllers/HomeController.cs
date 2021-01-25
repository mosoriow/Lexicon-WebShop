using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Core.Contracts;
using WebShop.Core.Models;
using WebShop.Core.ViewModels;

namespace WebShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> suggestedProducts;
        IRepository<Product> latestProducts;

        public HomeController(IRepository<Product> suggestedProductsContext, IRepository<Product> latestProductsContext)
        {
            suggestedProducts = suggestedProductsContext;
            latestProducts = latestProductsContext;
        }

        public ActionResult Index()
        {
            HomeProductListViewModel model = new HomeProductListViewModel();

            List<Product> suggestedList = suggestedProducts.Collection().ToList();
            List<Product> latestList = latestProducts.Collection().ToList();

            model.SuggestedProducts = suggestedList;
            model.LatestProducts = latestList;

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}