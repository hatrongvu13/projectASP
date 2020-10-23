using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Entity;
using Models.Models;
using projectASP03.Areas.Admin.Data;

namespace projectASP03.Areas.Admin.Controllers
{
    public class tourImgsController : Controller
    {
        private tourDAO dao = new tourDAO();
        private bool CheckSession()
        {
            UserSession user = SessionHelper.GetSession();
            return (user == null || user.UserName == null);
        }
        // GET: Admin/tourImgs
        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            var tourImgs = dao.GetImgByTourId(id);
            return View(tourImgs);
        }
        [HttpPost]
        public ActionResult Index(int id)
        {
            var tourImgs = dao.GetImgByTourId(id);
            return View(tourImgs);
        }

        // GET: Admin/tourImgs/Details/5
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
            tourImg tourImg = dao.GetDetailImg(id);
            if (tourImg == null)
            {
                return HttpNotFound();
            }
            return View(tourImg);
        }

        // GET: Admin/tourImgs/Create
        public ActionResult Create()
        {
            ViewBag.tourID = new SelectList(dao.GetAllTour(), "tourID", "name");
            return View();
        }

        // POST: Admin/tourImgs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tourID,link,main")] tourImg tourImg)
        {
            if (ModelState.IsValid)
            {
                dao.AddImgTour(tourImg);
                return RedirectToAction("Index");   
            }

            ViewBag.tourID = new SelectList(dao.GetAllTour(), "tourID", "name", tourImg.tourID);
            return View(tourImg);
        }

        // GET: Admin/tourImgs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tourImg tourImg = dao.GetDetailImg(id);
            if (tourImg == null)
            {
                return HttpNotFound();
            }
            ViewBag.tourID = new SelectList(dao.GetAllTour(), "tourID", "name", tourImg.tourID);
            return View(tourImg);
        }

        // POST: Admin/tourImgs/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( tourImg tourImg)
        {
            if (ModelState.IsValid)
            {
                dao.EditImgTour(tourImg);
                return RedirectToAction("Index");
            }
            ViewBag.tourID = new SelectList(dao.GetAllTour(), "tourID", "name", tourImg.tourID);
            return View(tourImg);
        }

        // GET: Admin/tourImgs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tourImg tourImg = dao.GetDetailImg(id);
            if (tourImg == null)
            {
                return HttpNotFound();
            }
            return View(tourImg);
        }

        // POST: Admin/tourImgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tourImg tourImg = dao.GetDetailImg(id);
            dao.RemoveImgTour(tourImg);
            return RedirectToAction("Index");
        }
        // UpLoad
        [HttpGet]
        public ActionResult UpLoad(int id)
        {
            var res = dao.GetDetailImg(id);
            return View(res);
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, tourImg tourImg)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    // check path exits
                    if (!Directory.Exists(Server.MapPath("~/Content/photos/tour")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/photos/tour"));
                    }
                    string _path = Path.Combine(Server.MapPath("~/Content/photos/tour"), _FileName);
                    // save file
                    file.SaveAs(_path);
                    tourImg.link = _FileName;
                    dao.EditImgTour(tourImg);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                //tourImg tourIm = dao.GetDetailImg(tourImg.id);
                return RedirectToAction("Index","tours");
            }
            catch
            {   
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }

    }
}
