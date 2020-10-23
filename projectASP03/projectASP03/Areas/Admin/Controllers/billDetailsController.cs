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
    public class billDetailsController : Controller
    {
        private billDAO dao = new billDAO();
        private bool CheckSession()
        {
            UserSession user = SessionHelper.GetSession();
            return (user == null || user.UserName == null);
        }
        // GET: Admin/billDetails
        public ActionResult Index()
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            var billDetails = dao.GetAllBillDetails();
            return View(billDetails);
        }

        // GET: Admin/billDetails/Details/5
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
            billDetail billDetail = dao.GetBillDetailsByID(id);
            if (billDetail == null)
            {
                return HttpNotFound();
            }
            return View(billDetail);
        }

        // GET: Admin/billDetails/Create
        public ActionResult Create()
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.idBill = new SelectList(dao.GetBills(), "id", "payment");
            ViewBag.tourID = new SelectList(dao.GetTours(), "tourID", "name");
            return View();
        }

        // POST: Admin/billDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idBill,tourID,pay,start,finish")] billDetail billDetail)
        {
            if (ModelState.IsValid)
            {
                dao.AddBillDetails(billDetail);
                return RedirectToAction("Index");
            }

            ViewBag.idBill = new SelectList(dao.GetBills(), "id", "payment", billDetail.idBill);
            ViewBag.tourID = new SelectList(dao.GetTours(), "tourID", "name", billDetail.tourID);
            return View(billDetail);
        }

        // GET: Admin/billDetails/Edit/5
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
            billDetail billDetail = dao.GetBillDetailsByID(id);
            if (billDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.idBill = new SelectList(dao.GetBills(), "id", "payment", billDetail.idBill);
            ViewBag.tourID = new SelectList(dao.GetTours(), "tourID", "name", billDetail.tourID);
            return View(billDetail);
        }

        // POST: Admin/billDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idBill,tourID,pay,start,finish")] billDetail billDetail)
        {
            if (ModelState.IsValid)
            {
                dao.EditBillDetails(billDetail);
                return RedirectToAction("Index");
            }
            ViewBag.idBill = new SelectList(dao.GetBills(), "id", "payment", billDetail.idBill);
            ViewBag.tourID = new SelectList(dao.GetTours(), "tourID", "name", billDetail.tourID);
            return View(billDetail);
        }

        // GET: Admin/billDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            billDetail billDetail = dao.GetBillDetailsByID(id);
            if (billDetail == null)
            {
                return HttpNotFound();
            }
            return View(billDetail);
        }

        // POST: Admin/billDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            billDetail billDetail = dao.GetBillDetailsByID(id);
            dao.RemoveBillDetails(billDetail);
            return RedirectToAction("Index");
        }
    }
}
