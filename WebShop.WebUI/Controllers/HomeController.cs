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
        IRepository<Product> listOfProducts;

        public HomeController(IRepository<Product> suggestedProductsContext, IRepository<Product> latestProductsContext, IRepository<Product> listOfProductsContext)
        {
            suggestedProducts = suggestedProductsContext;
            latestProducts = latestProductsContext;
            listOfProducts = listOfProductsContext;
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

        public ActionResult ProductList()
        {
            HomeProductListViewModel model = new HomeProductListViewModel();
            List<Product> productList = listOfProducts.Collection().ToList();

            model.ListOfProducts = productList;

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