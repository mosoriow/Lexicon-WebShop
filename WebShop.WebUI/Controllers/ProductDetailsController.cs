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
    public class ProductDetailsController : Controller
    {

        IRepository<Product> context;

        public ProductDetailsController(IRepository<Product> context)
        {
            this.context = context;
        }

        //get product from database include image and user reviews
        public Product GetProduct(String Id)
        {
            return context
                .Include(p => p.Images, p => p.UserReviews)
                .FirstOrDefault(p => p.Id == Id);
        }

        // /ProductDetails/Index/Id
        public ActionResult Index(String Id)
        {
            if (Id == null) Id = "1";

            //Find the Product including images
            Product product = GetProduct(Id);

            //todo: find the related image based on same category
            List<Product> relatedProducts = context.Include(p => p.Images)
                .Where(p => p.Category == product.Category)
                .ToList();

            ProductDetailsViewModel productDetailsViewModel = new ProductDetailsViewModel();
            productDetailsViewModel.product = product;
            productDetailsViewModel.relatedProducts = relatedProducts;
            return View(productDetailsViewModel);
        }

        [HttpPost] //add user review to db
        public ActionResult UpdateReview(ProductDetailsViewModel viewModel)
        {
            //update view model with username (since it is not in session), find the product and update review
            viewModel.userReview.UserName = "Anonymous";
            String Id = viewModel.product.Id;
            Product product = GetProduct(Id);
            product.UserReviews.Add(viewModel.userReview);
            context.Update(product);
            context.Commit();

            return RedirectToAction("Index", new { id = Id });
        }

        //partial view for side filter,  product is not mandatory, if given selected value heighlights
        public PartialViewResult ProductFilter(Product product)
        {
            ProductFilterViewModel model = new ProductFilterViewModel();
            if (product != null)
                model.SelectedProduct = product;
            model.subCategories = context.Collection().Select(p => p.SubCategory).Distinct();
            model.manufactures = context.Collection().Select(p => p.Manufacture).Distinct();
            List<Product> products = context.Include(p => p.Images)
               .ToList();
            model.products = products;
            return PartialView(model);
        }

    }
}