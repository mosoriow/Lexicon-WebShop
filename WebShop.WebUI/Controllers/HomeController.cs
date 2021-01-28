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

    }
}