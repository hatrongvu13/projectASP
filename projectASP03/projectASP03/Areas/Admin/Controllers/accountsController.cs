using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Models.DAO;
using Models.Entity;
using projectASP03.Areas.Admin.Data;

namespace projectASP03.Areas.Admin.Controllers
{
    public class accountsController : Controller
    {
        private accountDAO dao = new accountDAO();
        private bool CheckSession()
        {
            UserSession user = SessionHelper.GetSession();
            return (user == null || user.UserName == null);
        }
        // GET: Admin/accounts
        public ActionResult Index()
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            
            var res = dao.GetAllAccount();
            return View(res);
        }


        // GET: Admin/accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = dao.GetAccountByID(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Admin/accounts/Create
        public ActionResult Create()
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        // POST: Admin/accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "accountID,email,username,password")] account account)
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            if (ModelState.IsValid)
            {
                dao.AddNewAccount(account);
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: Admin/accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = dao.GetAccountByID(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Admin/accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "accountID,email,username,password")] account account)
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            if (ModelState.IsValid)
            {
                dao.EditAccount(account);
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Admin/accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = dao.GetAccountByID(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Admin/accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            account account = dao.GetAccountByID(id);
            dao.RemoveAccount(account);
            return RedirectToAction("Index");
        }
    }
}
