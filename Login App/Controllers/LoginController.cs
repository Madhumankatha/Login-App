using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login_App.Controllers
{
    public class LoginController : Controller
    {
        private db_demoEntities db = new db_demoEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(user user)
        {
            bool result = db.users.Any(u => u.username.Equals(user.username) && u.password.Equals(user.password));

            if (result)
            {
                ViewBag.Message = "Login Successfull!!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Incorrect Username or password!!";
            }

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(user user)
        {
            db.users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index","Login");
        }

    }
}