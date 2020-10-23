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
using PagedList;
using projectASP03.Areas.Admin.Data;

namespace projectASP03.Areas.Admin.Controllers
{
    public class toursController : Controller
    {
        private tourDAO dao = new tourDAO();
        private bool CheckSession()
        {
            UserSession user = SessionHelper.GetSession();
            return (user == null || user.UserName == null);
        }
        // GET: Admin/tours
        public ActionResult Index(int ? page)
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            //var tours = db.tours.Include(t => t.vacation);

            //return View(tours.ToList());
            var res = dao.GetAllTour();
            int PageNumber = (page ?? 1);
            return View(res.ToPagedList(PageNumber, 8));
        }

        // GET: Admin/tours/Details/5
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
            tour tour = dao.GetTourById(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // GET: Admin/tours/Create
        public ActionResult Create()
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.vacationID = new SelectList(dao.GetVacations(), "vacationID", "name");
            return View();
        }

        // POST: Admin/tours/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tourID,vacationID,name,start,finish,status,unit,sales")] tour tour)
        {
            if (ModelState.IsValid)
            {
                dao.AddNewTour(tour);
                return RedirectToAction("Index");
            }

            ViewBag.vacationID = new SelectList(dao.GetVacations(), "vacationID", "name", tour.vacationID);
            return View(tour);
        }

        // GET: Admin/tours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour tour = dao.GetTourById(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.vacationID = new SelectList(dao.GetVacations(), "vacationID", "name", tour.vacationID);
            return View(tour);
        }

        // POST: Admin/tours/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tourID,vacationID,name,start,finish,status,unit,sales")] tour tour)
        {
            if (ModelState.IsValid)
            {
                dao.EditTour(tour);
                return RedirectToAction("Index");
            }
            ViewBag.vacationID = new SelectList(dao.GetVacations(), "vacationID", "name", tour.vacationID);
            return View(tour);
        }

        // GET: Admin/tours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour tour = dao.GetTourById(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Admin/tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tour tour = dao.GetTourById(id);
            dao.RemoveTour(tour);
            return RedirectToAction("Index");
        }
    }
}
