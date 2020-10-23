using Models.DAO;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projectASP03.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        //GET: sales
        public PartialViewResult Sales()
        {
            tourDAO dao = new tourDAO();
            var res = dao.GetTourSales();
            return PartialView(res);
        }
        public ActionResult Search(SearchModels search, int? news, int ? price, int ? people)
        {
            int newsor = (news ?? 0);
            int priceor = (price ?? 0);
            ViewBag.newsSort = newsor;
            ViewBag.priceSort = priceor;
            ViewBag.search = search;
            tourDAO dao = new tourDAO();
            return View(dao.Search(search));  
        }

    }
}