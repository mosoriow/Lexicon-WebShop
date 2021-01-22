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

            if(product == null)
            {
                createProducts();
                createMatchingProducts();
            }
            else
            {
                //context.Update(product);
                //context.Commit();
            }

            ProductDetailsViewModel productDetailsViewModel = new ProductDetailsViewModel();
            productDetailsViewModel.product = product;
            productDetailsViewModel.relatedProducts = relatedProducts;
            productDetailsViewModel.subCategories = context.Collection().Select(p => p.SubCategory).Distinct();
            productDetailsViewModel.manufactures = context.Collection().Select(p => p.Manufacture).Distinct();
            return View(productDetailsViewModel);
        }

        private void createProducts()
        {
            Product product = new Product();
            product.Id = "1";
            product.Name = "Lång omlottklänning";
            product.Description = "A calf-length dress in jacquard-woven quality. The dress has a collar and buttoning at the front and a long sleeve with a cuff that has a buttoning. Rounded at the bottom with a slit in the sides. Unlined. ";
            product.Images.Add(new Image("/Content/productImages/ladies/1.jpg"));
            product.Images.Add(new Image("/Content/productImages/ladies/2.jpg"));
            product.Images.Add(new Image("/Content/productImages/ladies/3.jpg"));
            product.Images.Add(new Image("/Content/productImages/ladies/4.jpg"));
            product.Images.Add(new Image("/Content/productImages/ladies/5.jpg"));
            product.Category = "Woman";
            product.SubCategory = "Dresses";
            product.Manufacture = "Kathmandu";
            product.Price = 587.50m;
            product.Availability = 10;
            product.Colour = "Orange, Yellow";
            product.Size = "Large, Medium, Small, X-Large";

            //add user review
            product.UserReviews.Add(new UserReview("Nikhil", "This was nice in buy, Excelent product, Delevered on time, and Long lasting ", 5));
            product.UserReviews.Add(new UserReview("Hitha Hareendran", "This was nice in buy, Excelent product, Delevered on time, and Long lasting ", 4));
            product.UserReviews.Add(new UserReview("Mosorio", "This was nice in buy, Excelent product, Delevered on time, and Long lasting ", 3));

            context.Insert(product);
            context.Commit();
        }

        private void createMatchingProducts()
        {
            createMatchingProducts("2", "/Content/productImages/ladies/2.jpg", "Pants", 341);
            createMatchingProducts("3", "/Content/productImages/ladies/3.jpg", "Shirts", 124);
            createMatchingProducts("4", "/Content/productImages/ladies/4.jpg", "Paijamas", 200);
            createMatchingProducts("5", "/Content/productImages/ladies/5.jpg", "Dresses", 75);
            createMatchingProducts("6", "/Content/productImages/ladies/6.jpg", "Pants", 899);
            createMatchingProducts("7", "/Content/productImages/ladies/7.jpg", "Shirts", 222);
            createMatchingProducts("8", "/Content/productImages/ladies/8.jpg", "Paijamas", 456);
            createMatchingProducts("9", "/Content/productImages/ladies/9.jpg", "Dresses", 333);
        }

        private void createMatchingProducts(String id, String image, String subCategory, decimal price )
        {
            Product product = new Product();
            product.Id = id;
            product.Name = "A-linjeklänning";
            product.Description = "A calf-length dress in jacquard-woven quality. The dress has a collar and buttoning at the front and a long sleeve with a cuff that has a buttoning. Rounded at the bottom with a slit in the sides. Unlined. ";
            product.Images.Add(new Image(image));

            product.Category = "Woman";
            product.SubCategory = subCategory;
            product.Manufacture = "Kathmandu";
            product.Price = price;
            product.Availability = 10;
            product.Colour = "Orange, Yellow";
            product.Size = "Large, Medium, Small, X-Large";
            context.Insert(product);
            context.Commit();
        }

        [HttpPost]
        public ActionResult UpdateReview(ProductDetailsViewModel viewModel)
        {
            //update view model with username (since it is not in session), find the product and update review
            viewModel.userReview.UserName = "Unknown";
            String Id = viewModel.product.Id;
            Product product = GetProduct(Id);
            product.UserReviews.Add(viewModel.userReview);
            context.Update(product);
            context.Commit();

            return RedirectToAction("Index", new { id = Id });
        }

        

    }
}