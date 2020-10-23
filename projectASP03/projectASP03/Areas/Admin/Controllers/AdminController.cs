using Models.DAO;
using Models.Models;
using projectASP03.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace projectASP03.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private bool CheckSession()
        {
            UserSession user = SessionHelper.GetSession();
            return (user == null || user.UserName == null);
        }
        // GET: Admin/Admin
        public ActionResult Index()
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModels model)
        {
            var res = new accountDAO();
            var check = res.Login(model.email, model.password);
            if(check && ModelState.IsValid)
            {
                SessionHelper.SetSession(new UserSession() { UserName = model.email });
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Ten dang nhap hoac mat khau khong dung.");
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            SessionHelper.SetSession(new UserSession());
            return RedirectToAction("Login", "Admin");
        }
    }
}
