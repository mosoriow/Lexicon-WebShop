using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebShop.WebUI.Models;
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
            createInitialDataIfNotExist();

            HomeProductListViewModel model = new HomeProductListViewModel();

            List<Product> suggestedList = suggestedProducts.Include(p=>p.Images).ToList();
            List<Product> latestList = latestProducts.Include(p => p.Images).ToList();

            model.SuggestedProducts = suggestedList;
            model.LatestProducts = latestList;

            return View(model);
        }

        public ActionResult ProductList()
        {
            HomeProductListViewModel model = new HomeProductListViewModel();
            List<Product> productList = listOfProducts.Include(p => p.Images).ToList();

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
            ViewBag.Message = "Your application description page.";

            return View();
        }



        public static void SendEmail(string name,string email,string telephon,string usermessage)
        {
            string to = email;
            string subject = "subject1";
            string message = usermessage;
            string from = "noreply@ahoora.se";
            string username = "noreply@ahoora.se";
            string password = "AwSeDr@!220";
            string host = "send.one.com";
            int port = 587;
        
            var loginInfo = new NetworkCredential(username, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient(host, port);

            msg.From = new MailAddress(from);
            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }

        [HttpPost]
        public ActionResult Contact(MailModels e)
        {
            if (ModelState.IsValid)
            {
                 //prepare email
                SendEmail(e.Name,e.Email,e.Telephone,e.Message);
            }
            return View();
        }

        
       
        public ActionResult SuccessMessage()
        {
            return View();
        }


        /*  Initialize database with default product data */
        private void createInitialDataIfNotExist()
        {
            if (suggestedProducts.Collection().Count() == 0)
            {
                createProducts();
                createMatchingProducts();
                createAdditionalCategories();
                createAdditionalCategories1();
            }
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
            product.Discount = 10;

            //add user review
            product.UserReviews.Add(new UserReview("Nikhil", "This was nice in buy, Excelent product, Delevered on time, and Long lasting ", 5));
            product.UserReviews.Add(new UserReview("Hitha Hareendran", "This was nice in buy, Excelent product, Delevered on time, and Long lasting ", 4));
            product.UserReviews.Add(new UserReview("Mosorio", "This was nice in buy, Excelent product, Delevered on time, and Long lasting ", 3));

            suggestedProducts.Insert(product);
            suggestedProducts.Commit();
        }

        private void createMatchingProducts()
        {
            createMatchingProducts("2", "/Content/productImages/ladies/2.jpg", "Woman", "Dresses", "HERMES", 341,0);
            createMatchingProducts("3", "/Content/productImages/ladies/3.jpg", "Woman", "Dresses", "HERMES", 124,5);
            createMatchingProducts("4", "/Content/productImages/ladies/4.jpg", "Woman", "Dresses", "PRADA", 200,5);
            createMatchingProducts("5", "/Content/productImages/ladies/5.jpg", "Woman", "Dresses", "PRADA", 75,0);
            createMatchingProducts("6", "/Content/productImages/ladies/6.jpg", "Woman", "Dresses", "CHANEL", 899,0);
            createMatchingProducts("7", "/Content/productImages/ladies/7.jpg", "Woman", "Dresses", "CHANEL", 222,10);
            createMatchingProducts("8", "/Content/productImages/ladies/8.jpg", "Woman", "Dresses", "BURBERRY", 456,15);
            createMatchingProducts("9", "/Content/productImages/ladies/9.jpg", "Woman", "Dresses", "ARMANI", 333,20);
        }

        private void createAdditionalCategories()
        {
            createMatchingProducts("10", "/Content/productImages/accessories/10.jfif", "Woman", "Accessories", "HERMES", 341,0);
            createMatchingProducts("11", "/Content/productImages/accessories/11.jfif", "Woman", "Accessories", "PRADA", 341,0);
           
            createMatchingProducts("20", "/Content/productImages/shirts/20.jfif", "Woman", "Shirts", "CHANEL", 341,0);
            createMatchingProducts("21", "/Content/productImages/shirts/21.jfif", "Woman", "Shirts", "PRADA", 341,0);

            createMatchingProducts("30", "/Content/productImages/pants/30.jfif", "Woman", "Pants", "BURBERRY", 341,0);
            createMatchingProducts("31", "/Content/productImages/pants/31.jfif", "Woman", "Pants", "BURBERRY", 341,5);

            createMatchingProducts("40", "/Content/productImages/Pijamas/40.jfif", "Woman", "Pijamas", "BURBERRY", 341,5);
            createMatchingProducts("41", "/Content/productImages/Pijamas/41.jfif", "Woman", "Pijamas", "BURBERRY", 341,5);

            createMatchingProducts("50", "/Content/productImages/shoes/50.jfif", "Woman", "Shoes", "BURBERRY", 341,5);
            createMatchingProducts("51", "/Content/productImages/shoes/51.jfif", "Woman", "Shoes", "BURBERRY", 341,5);
            
        }

        private void createAdditionalCategories1()
        {
            createMatchingProducts("12", "/Content/productImages/24c77441-273b-4f80-b309-f237bb730e84.jpg", "Woman", "Accessories", "HERMES", 341, 10);
            createMatchingProducts("13", "/Content/productImages/58b95153-74e3-4b99-9bb5-bf8e66696b9a.jpg", "Woman", "Accessories", "PRADA", 341, 10);

            createMatchingProducts("14", "/Content/productImages/5fa30349-7fa0-4e72-98ed-6aedc07dcbe5.jpg", "Woman", "Accessories", "CHANEL", 341, 10);
            createMatchingProducts("15", "/Content/productImages/6c5cde4d-b857-41f6-8520-0463339baabc.jpg", "Woman", "Accessories", "PRADA", 341, 10);

            createMatchingProducts("16", "/Content/productImages/8a85af5b-5670-4af3-983b-26677e55a36a.jpg", "Woman", "Accessories", "BURBERRY", 341, 10);
            createMatchingProducts("17", "/Content/productImages/9a21021a-e5d9-46db-a801-bc6fd9eb3d5d.jpg", "Woman", "Accessories", "BURBERRY", 341, 5);

            createMatchingProducts("18", "/Content/productImages/9da87f20-dd3a-4bb5-9678-759518deccec.jpg", "Woman", "Accessories", "BURBERRY", 341, 5);
            createMatchingProducts("19", "/Content/productImages/e478dd5d-7289-4f0d-9d03-e79a91989556.jpg", "Woman", "Accessories", "BURBERRY", 341, 5);

            createMatchingProducts("22", "/Content/productImages/ec3fb52f-ec5f-436a-959c-0b25ad3e0286.jpg", "Woman", "Accessories", "BURBERRY", 341, 5);
            createMatchingProducts("23", "/Content/productImages/ee6520d7-cc52-4e6e-991b-46e5d70a8303.jpg", "Woman", "Accessories", "BURBERRY", 341, 5);

        }

        private void createMatchingProducts(String id, String image, String Category, String subCategory, String Manufacture, decimal price, int discount)
        {
            Product product = new Product();
            product.Id = id;
            product.Name = "A-linjeklänning";
            product.Description = "A calf-length dress in jacquard-woven quality. The dress has a collar and buttoning at the front and a long sleeve with a cuff that has a buttoning. Rounded at the bottom with a slit in the sides. Unlined. ";
            product.Images.Add(new Image(image));

            product.Category = Category;
            product.SubCategory = subCategory;
            product.Manufacture = Manufacture;
            product.Price = price;
            product.Availability = 10;
            product.Colour = "Orange, Yellow";
            product.Size = "Large, Medium, Small, X-Large";
            product.Discount = discount;
            suggestedProducts.Insert(product);
            suggestedProducts.Commit();
        }

    }
}