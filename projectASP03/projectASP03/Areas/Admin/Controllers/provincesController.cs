using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Entity;
using projectASP03.Areas.Admin.Data;

namespace projectASP03.Areas.Admin.Controllers
{
    public class provincesController : Controller
    {
        private vacationDAO dao = new vacationDAO();
        private bool CheckSession()
        {
            UserSession user = SessionHelper.GetSession();
            return (user == null || user.UserName == null);
        }
        // GET: Admin/provinces
        public ActionResult Index()
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(dao.GetProvinces());
        }

        // GET: Admin/provinces/Details/5
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
            province province = dao.GetProvinceByID(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        // GET: Admin/provinces/Create
        public ActionResult Create()
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        // POST: Admin/provinces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "provinceID,name")] province province)
        {
            if (ModelState.IsValid)
            {
                dao.AddProvinces(province);
                return RedirectToAction("Index");
            }

            return View(province);
        }

        // GET: Admin/provinces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            province province = dao.GetProvinceByID(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        // POST: Admin/provinces/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "provinceID,name")] province province)
        {
            if (ModelState.IsValid)
            {
                dao.EditProvinces(province);
                return RedirectToAction("Index");
            }
            return View(province);
        }

        // GET: Admin/provinces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            province province = dao.GetProvinceByID(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        // POST: Admin/provinces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            province province = dao.GetProvinceByID(id);
            dao.RemoveProvince(province);
            return RedirectToAction("Index");
        }
    }
}
