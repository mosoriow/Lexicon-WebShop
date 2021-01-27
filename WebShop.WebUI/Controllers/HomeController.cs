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

namespace WebShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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



      
        
    


        public static void SendEmail()
        {
            string to = "janalizade@gmail.com";
            string subject = "subject1";
            string message = "message1";
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
                SendEmail();
            }
            return View();
        }

        
       
        public ActionResult SuccessMessage()
        {
            return View();
        }

    }
}