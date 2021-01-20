using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Core.Contracts;
using WebShop.Core.Models;
using WebShop.DataAccess.InMemory;
using WebShop.DataAccess.SQL;
namespace WebShop.WebUI.Controllers
{
    public class UserController : Controller
    {
        DataContext db;

        public UserController()
        {
            db = new DataContext();

        }


        // GET: User
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


    }
}