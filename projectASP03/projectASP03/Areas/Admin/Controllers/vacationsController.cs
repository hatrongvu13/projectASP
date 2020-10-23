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
    public class vacationsController : Controller
    {
        private vacationDAO dao = new vacationDAO();
        private bool CheckSession()
        {
            UserSession user = SessionHelper.GetSession();
            return (user == null || user.UserName == null);
        }
        // GET: Admin/vacations
        public ActionResult Index()
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }

            return View(dao.GetVacations());
        }

        // GET: Admin/vacations/Details/5
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
            vacation vacation = dao.GetVacationByID(id);
            if (vacation == null)
            {
                return HttpNotFound();
            }
            return View(vacation);
        }

        // GET: Admin/vacations/Create
        public ActionResult Create()
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.provinceID = new SelectList(dao.GetProvinces(), "provinceID", "name");
            return View();
        }

        // POST: Admin/vacations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "vacationID,provinceID,name")] vacation vacation)
        {
            if (ModelState.IsValid)
            {
                dao.AddVacations(vacation);
                return RedirectToAction("Index");
            }

            ViewBag.provinceID = new SelectList(dao.GetProvinces(), "provinceID", "name", vacation.provinceID);
            return View(vacation);
        }

        // GET: Admin/vacations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vacation vacation = dao.GetVacationByID(id);
            if (vacation == null)
            {
                return HttpNotFound();
            }
            ViewBag.provinceID = new SelectList(dao.GetProvinces(), "provinceID", "name", vacation.provinceID);
            return View(vacation);
        }

        // POST: Admin/vacations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "vacationID,provinceID,name")] vacation vacation)
        {
            if (ModelState.IsValid)
            {
                dao.EditVacations(vacation);
                return RedirectToAction("Index");
            }
            ViewBag.provinceID = new SelectList(dao.GetProvinces(), "provinceID", "name", vacation.provinceID);
            return View(vacation);
        }

        // GET: Admin/vacations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vacation vacation = dao.GetVacationByID(id);
            if (vacation == null)
            {
                return HttpNotFound();
            }
            return View(vacation);
        }

        // POST: Admin/vacations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            vacation vacation = dao.GetVacationByID(id);
            dao.RemoveVacation(vacation);
            return RedirectToAction("Index");
        }
    }
}
