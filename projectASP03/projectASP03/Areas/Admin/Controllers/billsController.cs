using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.Entity;
using projectASP03.Areas.Admin.Data;

namespace projectASP03.Areas.Admin.Controllers
{
    public class billsController : Controller
    {
        private projectASP01Entities db = new projectASP01Entities();
        private bool CheckSession()
        {
            UserSession user = SessionHelper.GetSession();
            return (user == null || user.UserName == null);
        }
        // GET: Admin/bills
        public ActionResult Index()
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            var bills = db.bills.Include(b => b.customer);
            return View(bills.ToList());
        }

        // GET: Admin/bills/Details/5
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
            bill bill = db.bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // GET: Admin/bills/Create
        public ActionResult Create()
        {
            if (CheckSession())
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.customerID = new SelectList(db.customers, "customerID", "email");
            return View();
        }

        // POST: Admin/bills/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,customerID,amountOfPeople,child,payment,paystatus,confirm")] bill bill)
        {
            if (ModelState.IsValid)
            {
                db.bills.Add(bill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customerID = new SelectList(db.customers, "customerID", "email", bill.customerID);
            return View(bill);
        }

        // GET: Admin/bills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill bill = db.bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerID = new SelectList(db.customers, "customerID", "email", bill.customerID);
            return View(bill);
        }

        // POST: Admin/bills/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,customerID,amountOfPeople,child,payment,paystatus,confirm")] bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customerID = new SelectList(db.customers, "customerID", "email", bill.customerID);
            return View(bill);
        }

        // GET: Admin/bills/Delete/5
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
            bill bill = db.bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // POST: Admin/bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bill bill = db.bills.Find(id);
            db.bills.Remove(bill);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
