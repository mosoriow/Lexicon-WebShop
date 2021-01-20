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

        // /ProductDetails/Index/Id
        public ActionResult Index(String Id)
        {
            if (Id == null) Id = "1";

            //Find the Product including images
            Product product = context
                .Include(p => p.Images, p => p.UserReviews)
                .FirstOrDefault(p=>p.Id==Id);

            //todo: find the related image based on same category
            List<Product> relatedProducts = context.Include(p => p.Images)
                .Where(p => p.Category == product.Category)
                .ToList();

            if(product == null)
            {
                createProducts();
                createMatchingProducts();
            }

            ProductDetailsViewModel productDetailsViewModel = new ProductDetailsViewModel();
            productDetailsViewModel.product = product;
            productDetailsViewModel.relatedProducts = relatedProducts;

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

    }
}