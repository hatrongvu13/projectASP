using Models.Entity;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class tourDAO
    {
        private projectASP01Entities db = new projectASP01Entities();
        public List<tour> GetAllTour()
        {
            List<tour> list = db.tours.Include("vacation").ToList();
            return list;
        }
        public List<tour> GetTourSales()
        {
            List<tour> list = db.tours.Include("vacation").Where(t => t.sales > 0).Take(8).ToList();
            return list;
        }
        public tour GetTourById(int ? id)
        {
            tour res = db.tours.Find(id);
            return res;
        }
        public List<tour> GetTourOrderNews()
        {
            List<tour> list = db.tours.Include("vacation").OrderBy(t => t.start).ToList();
            return list;
        }
        public List<tour> GetTourOrderPrice()
        {
            List<tour> list = db.tours.Include("vacation").OrderBy(t => t.unit).ToList();
            return list;
        }
        public bool AddNewTour(tour tour)
        {
            try
            {
                db.tours.Add(tour);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool EditTour(tour tour)
        {
            try
            {
                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveTour(tour tour)
        {
            try
            {
                db.tours.Remove(tour);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // TourDetails
        public List<tourDetail> GetTourDetails(int ? id)
        {
            var res = db.tourDetails.Where(t=> t.tourID == id).ToList();
            if (res == null)
            {
                return null;
            }
            else
            {
                return res;
            }
        }
        public tourDetail GetDetailById(int? id)
        {
            return db.tourDetails.Find(id);
        }
        public bool AddTourDetail(tourDetail tourDetail)
        {
            try
            {
                db.tourDetails.Add(tourDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool EditDetails(tourDetail tourDetail)
        {
            try
            {
                db.Entry(tourDetail).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveDetails(tourDetail tourDetail)
        {
            try
            {
                db.tourDetails.Remove(tourDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        // ToutImg
        public List<tourImg> GetImgByTourId(int ? id)
        {
            List<tourImg> list = new List<tourImg>();
            var res = db.tourImgs.Where(i =>i.tourID == id);
            if(res == null)
            {
                return null;
            }
            else
            {
                list = res.ToList();
                return list;
            }
        }
        public List<tourImg> GetImg()
        {
            List<tourImg> list = new List<tourImg>();
            var res = db.tourImgs;
            if (res == null)
            {
                return null;
            }
            else
            {
                list = res.ToList();
                return list;
            }
        }
        public tourImg GetDetailImg(int? id)
        {
            return db.tourImgs.Find(id);
        }
        public bool AddImgTour(tourImg tourImg)
        {
            try
            {
                db.tourImgs.Add(tourImg);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool EditImgTour(tourImg tourImg)
        {
            try
            {
                db.Entry(tourImg).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveImgTour(tourImg tourImg)
        {
            try
            {
                db.tourImgs.Remove(tourImg);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        // description tour
        public List<descriptionTour> GetDescriptionTours()
        {
            return db.descriptionTours.ToList();
        }
        public descriptionTour GetDescriptionTourByID(int ? id)
        {
            return db.descriptionTours.Find(id);
        }
        public bool AddDescriptTours(descriptionTour descriptionTour)
        {
            try
            {
                db.descriptionTours.Add(descriptionTour);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool EditDescriptTour(descriptionTour descriptionTour)
        {
            try
            {
                db.Entry(descriptionTour).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveDescriptTour(descriptionTour descriptionTour)
        {
            try
            {
                db.descriptionTours.Remove(descriptionTour);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        // somethine else
        public List<tour> Search(SearchModels searchModels)
        {
            List<tour> list = new List<tour>();
            var res = db.tours.Where(t => t.name.Contains(searchModels.Tour));
            list = res.ToList();
            return list;
        }
        public List<vacation> GetVacations()
        {
            List<vacation> list = new List<vacation>();
            var res = db.vacations;
            list = res.ToList();
            return list;
        }
        public List<tourImg> ListImgByTourID(int id)
        {
            return db.tourImgs.Where(i => i.tourID == id).ToList();
        }
        public int GetUnit(int id)
        {
            tour t = GetTourById(id);
            if (t.sales != 0)
            {
                return ((int)(t.unit - (t.unit * t.sales / 100)));
            }
            else
            {
                return (int)t.unit;
            }
        }
        public string GetAmountDays(int id)
        {
            tour t = GetTourById(id);
            DateTime start = Convert.ToDateTime(t.start);
            DateTime end = Convert.ToDateTime(t.finish);
            TimeSpan tp = end.Subtract(start);
            //int time = Convert.ToInt32(tp);
            return tp.ToString("dd");
        }
    }
}
