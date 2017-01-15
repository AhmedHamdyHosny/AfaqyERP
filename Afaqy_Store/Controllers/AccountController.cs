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
        public bool Login(LoginViewModel model)
        {
            var user = model.Login();
            var success = false;
            if (user != null)
            {
                success = true;
                user.SaveUserToLocalStorage(model.RememberMe);
            }
            return success;
        }
    }
}