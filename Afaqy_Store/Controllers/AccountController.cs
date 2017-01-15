using Afaqy_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afaqy_Store.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account/login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Email,Password,RememberMe")] LoginViewModel model)
        {
            var user = model.Login();
            if (user != null)
            {
                user.SaveUserToLocalStorage(model.RememberMe);
               
                return RedirectToAction("Index", "Device");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}