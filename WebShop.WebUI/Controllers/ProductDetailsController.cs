﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Core.Models;
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

                // creating the product for testing todo : need to remove this
                if(product==null)
                {
                product = new Product();
                product.Id = "1";
                product.Name = "Lång omlottklänning";
                product.Description = "A calf-length dress in jacquard-woven quality. The dress has a collar and buttoning at the front and a long sleeve with a cuff that has a buttoning. Rounded at the bottom with a slit in the sides. Unlined. ";
                product.Images.Add(new Image("/Content/productImages/1/1.jpg"));
                product.Images.Add(new Image("/Content/productImages/1/2.jpg"));
                product.Images.Add(new Image("/Content/productImages/1/3.jpg"));
                product.Images.Add(new Image("/Content/productImages/1/4.jpg"));
                product.Images.Add(new Image("/Content/productImages/1/5.jpg"));
                product.Category = "Woman";
                product.SubCategory = "Dresses";
                product.Manufacture = "Kathmandu";
                product.Price = 587.50m;
                product.Availability = 10;
                context.Insert(product);
                context.Commit();
                }
            else
            {
                product.Colour = "Orange, Yellow";
                product.Size = "Large, Medium, Small, X-Large";
                context.Update(product);
                context.Commit();
            }

                return View(product);
        }

    }
}