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
    public class tourDetailsController : Controller
    {
        private tourDAO dao = new tourDAO();
        private bool CheckSession()
        {
            UserSession user = SessionHelper.GetSession();
            return (user == null || user.UserName == null);
        }
        public ActionResult Index(int ? page, int? id)
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            var tourDetails = dao.GetTourDetails(id);
            return View(tourDetails);
        }
        //GET: Admin/tourDetails
       [HttpPost]
        public ActionResult Index(int id)
        {
            var tourDetails = dao.GetTourDetails(id);
            return View(tourDetails);
        }

        // GET: Admin/tourDetails/Details/5
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
            tourDetail tourDetail = dao.GetDetailById(id);
            if (tourDetail == null)
            {
                return HttpNotFound();
            }
            return View(tourDetail);
        }

        // GET: Admin/tourDetails/Create
        public ActionResult Create()
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.tourID = new SelectList(dao.GetAllTour(), "tourID", "name");
            return View();
        }

        // POST: Admin/tourDetails/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tourID,transport,hotel,amountOfPeople,childUnit,childAge")] tourDetail tourDetail)
        {
            if (ModelState.IsValid)
            {
                dao.AddTourDetail(tourDetail);
                return RedirectToAction("Index",new {id =tourDetail.tourID});
            }

            ViewBag.tourID = new SelectList(dao.GetAllTour(), "tourID", "name", tourDetail.tourID);
            return View(tourDetail);
        }

        // GET: Admin/tourDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tourDetail tourDetail = dao.GetDetailById(id);
            if (tourDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.tourID = new SelectList(dao.GetAllTour(), "tourID", "name", tourDetail.tourID);
            return View(tourDetail);
        }

        // POST: Admin/tourDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,tourID,transport,hotel,amountOfPeople,childUnit,childAge")] tourDetail tourDetail)
        {
            if (ModelState.IsValid)
            {
                dao.EditDetails(tourDetail);
                return RedirectToAction("Index",new {id= tourDetail.tourID });
            }
            ViewBag.tourID = new SelectList(dao.GetAllTour(), "tourID", "name", tourDetail.tourID);
            return View(tourDetail);
        }

        // GET: Admin/tourDetails/Delete/5
        public ActionResult Delete(int? id, int? tourID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tourDetail tourDetail = dao.GetDetailById(id);
            if (tourDetail == null)
            {
                return HttpNotFound();
            }
            return View(tourDetail);
        }

        // POST: Admin/tourDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int tourID)
        {
            tourDetail tourDetail = dao.GetDetailById(id);
            dao.RemoveDetails(tourDetail);
            return RedirectToAction("Index", new {id = tourID });
        }

    }
}
