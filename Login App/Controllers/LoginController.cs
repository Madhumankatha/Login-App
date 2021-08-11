using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
                user loggedUser = db.users.First(u => u.username.Equals(user.username) && u.password.Equals(user.password));
                Session["username"] = loggedUser.username;
                Session["userid"] = loggedUser.id;

                FormsAuthentication.SetAuthCookie(loggedUser.username, false);


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

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["username"] = null;
            Session["userid"] = null;
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                // Verification.    
                if (Url.IsLocalUrl(returnUrl))
                {
                    // Info.    
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
            // Info.    
            return this.RedirectToAction("Index", "Home");
        }

    }
}