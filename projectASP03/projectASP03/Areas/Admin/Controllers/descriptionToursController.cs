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
    public class descriptionToursController : Controller
    {
        private tourDAO dao= new tourDAO();
        private bool CheckSession()
        {
            UserSession user = SessionHelper.GetSession();
            return (user == null || user.UserName == null);
        }
        // GET: Admin/descriptionTours
        public ActionResult Index()
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(dao.GetDescriptionTours());
        }

        // GET: Admin/descriptionTours/Details/5
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
            descriptionTour descriptionTour = dao.GetDescriptionTourByID(id);
            if (descriptionTour == null)
            {
                return HttpNotFound();
            }
            return View(descriptionTour);
        }

        // GET: Admin/descriptionTours/Create
        public ActionResult Create()
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.tourID = new SelectList(dao.GetAllTour(), "tourID", "name");
            return View();
        }

        // POST: Admin/descriptionTours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tourID,title,content")] descriptionTour descriptionTour)
        {
            if (ModelState.IsValid)
            {
                dao.AddDescriptTours(descriptionTour);
                return RedirectToAction("Index");
            }

            ViewBag.tourID = new SelectList(dao.GetAllTour(), "tourID", "name", descriptionTour.tourID);
            return View(descriptionTour);
        }

        // GET: Admin/descriptionTours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            descriptionTour descriptionTour = dao.GetDescriptionTourByID(id);
            if (descriptionTour == null)
            {
                return HttpNotFound();
            }
            ViewBag.tourID = new SelectList(dao.GetAllTour(), "tourID", "name", descriptionTour.tourID);
            return View(descriptionTour);
        }

        // POST: Admin/descriptionTours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,tourID,title,content")] descriptionTour descriptionTour)
        {
            if (ModelState.IsValid)
            {
                dao.EditDescriptTour(descriptionTour);
                return RedirectToAction("Index");
            }
            ViewBag.tourID = new SelectList(dao.GetAllTour(), "tourID", "name", descriptionTour.tourID);
            return View(descriptionTour);
        }

        // GET: Admin/descriptionTours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            descriptionTour descriptionTour = dao.GetDescriptionTourByID(id);
            if (descriptionTour == null)
            {
                return HttpNotFound();
            }
            return View(descriptionTour);
        }

        // POST: Admin/descriptionTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            descriptionTour descriptionTour = dao.GetDescriptionTourByID(id);
            dao.RemoveDescriptTour(descriptionTour);
            return RedirectToAction("Index");
        }

        
    }
}
