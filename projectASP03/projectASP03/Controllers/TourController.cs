using Models.DAO;
using Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projectASP03.Controllers
{
    public class TourController : Controller
    {
        // GET: Tour
        public ActionResult Index(int ? sale, int ? page)
        {
            List<tour> list = new List<tour>();
            tourDAO dao = new tourDAO();
            if (sale != null) {
                list = dao.GetTourSales();
            }
            else
            {
                list = dao.GetAllTour();
            }
            int PageNumber = (page ?? 1);
            int pageSize = 8;
            return View(list.ToPagedList(PageNumber, pageSize));
        }

        // GET: Tour/Details/5
        public ActionResult Details(int id) 
        {
            tourDAO dao = new tourDAO();
            ViewBag.TourImg = dao.ListImgByTourID(id);
            ViewBag.TourDetail = dao.GetDescriptionTourByID(id);
            var tour = dao.GetTourById(id);
            ViewBag.price = dao.GetUnit(id);
            ViewBag.totalDay = dao.GetAmountDays(id);
            return View(tour);
        }
    }   
}
