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
    [Authorize]
    public class WishListController : Controller
    {
        IRepository<WishList> context;
        IRepository<Product> productContext;

        public WishListController(IRepository<WishList> context, IRepository<Product> productContext)
        {
            this.context = context;
            this.productContext = productContext;
        }
        // GET: WishList
        public ActionResult Index()
        {
            /* fill the data from previous order if any */
            String userName = User.Identity.Name;
            var wishlist = context.Collection()
                .Where(w => w.UserName == userName)
                .Select(w => w.productId)
                .ToList();
            List<Product> wishlistItems = new List<Product>();
            foreach (var productId in wishlist)
            {
                wishlistItems.Add(productContext.Include(p=>p.Images).Where(p=>p.Id==productId).FirstOrDefault());
            }
               
            return View(wishlistItems);
        }

        //delete button on wishlist
        public ActionResult RemoveFromWishList(String Id)
        {
            /* fill the data from previous order if any */
            String userName = User.Identity.Name;
            var wishlist = context.Collection()
              .Where(w => w.UserName == userName && w.productId == Id)
              .FirstOrDefault();
            context.Delete(wishlist.Id);
            context.Commit();
            return RedirectToAction("Index");
        }

        //add to cart from wishlist
        public ActionResult AddToCartFromWishList(String Id)
        {
            /* fill the data from previous order if any */
            String userName = User.Identity.Name;
            var wishlist = context.Collection()
              .Where(w => w.UserName == userName && w.productId == Id)
              .FirstOrDefault();
            context.Delete(wishlist.Id);
            context.Commit();


            return RedirectToAction("AddToBasket","Basket", new { productid = Id });
        }



        [HttpPost]
        //add to wishlist
        public ActionResult AddToWishList(String productId)
        {
            String userName = User.Identity.Name;
            var wishlist = context.Collection()
                .Where(w => w.UserName == userName && w.productId == productId)
                .FirstOrDefault();

            if(wishlist==null)
            {
                wishlist = new WishList();
                wishlist.UserName = userName;
                wishlist.productId = productId;
                context.Insert(wishlist);
                context.Commit();
            }
            return RedirectToAction("Index");
        }


    }

}