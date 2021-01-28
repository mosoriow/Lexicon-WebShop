using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Core.Models;
using WebShop.Core.ViewModels;
using WebShop.Core.Contracts;
using System.Diagnostics;

namespace WebShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<Image> productImages;

        public ProductManagerController(IRepository<Product> productContext,IRepository<Image> prodImages)
        {
            context = productContext;
            productImages = prodImages;
        }

        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.Images= productImages.Collection();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(
                                Product product,
                                HttpPostedFileBase file,
                                HttpPostedFileBase file2, 
                                HttpPostedFileBase file3, 
                                HttpPostedFileBase file4
                            )
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if(file != null)
                {
                    product.filePath1 = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//productImages//") + product.Id + Path.GetExtension(file.FileName));
                }
                if (file2 != null)
                {
                    product.filePath2 = product.Id + Path.GetExtension(file2.FileName);
                    file2.SaveAs(Server.MapPath("//Content//productImages//") + product.Id +"_2"+ Path.GetExtension(file2.FileName));
                }
                if (file3 != null)
                {
                    product.filePath3 = product.Id + Path.GetExtension(file3.FileName);
                    file3.SaveAs(Server.MapPath("//Content//productImages//") + product.Id + "_3" + Path.GetExtension(file3.FileName));
                }
                if (file4 != null)
                {
                    product.filePath4 = product.Id + Path.GetExtension(file4.FileName);
                    file4.SaveAs(Server.MapPath("//Content//productImages//") + product.Id + "_4" + Path.GetExtension(file4.FileName));
                }


                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = new Product();
                viewModel.Images = productImages.Collection();

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(
                                Product product, 
                                string Id,
                                HttpPostedFileBase file/*,
                                HttpPostedFile file2,
                                HttpPostedFile file3,
                                HttpPostedFile file4*/
                           )
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                if (file != null)
                {
                    //product.Images = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//productImages//") + product.Id + Path.GetExtension(file.FileName));
                }
                /*
                if (file2 != null)
                {
                    //product.Images = product.Id + Path.GetExtension(file.FileName);
                    file2.SaveAs(Server.MapPath("//Content//productImages//") + product.Id + "_2" + Path.GetExtension(file2.FileName));
                }
                if (file3 != null)
                {
                    //product.Images = product.Id + Path.GetExtension(file.FileName);
                    file3.SaveAs(Server.MapPath("//Content//productImages//") + product.Id + "_3" + Path.GetExtension(file3.FileName));
                }
                if (file4 != null)
                {
                    //product.Images = product.Id + Path.GetExtension(file.FileName);
                    file4.SaveAs(Server.MapPath("//Content//productImages//") + product.Id + "_4" + Path.GetExtension(file4.FileName));
                }
                */

                productToEdit.Name = product.Name;
                productToEdit.Description = product.Description;
                productToEdit.Manufacture = product.Manufacture;
                productToEdit.Price = product.Price;
                productToEdit.Discount = product.Discount;
                productToEdit.Category = product.Category;
                productToEdit.SubCategory = product.SubCategory;
                productToEdit.Images = product.Images;
                productToEdit.Size = product.Size;
                productToEdit.Colour = product.Colour;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost,ActionName("Delete")]
        public ActionResult confirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return View(productToDelete);
            }
        }
    }
}