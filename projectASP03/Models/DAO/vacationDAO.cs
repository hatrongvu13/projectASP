using Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class vacationDAO
    {
        private projectASP01Entities db = new projectASP01Entities();
        // vacation
        public List<vacation> GetVacations()
        {
            return db.vacations.ToList();
        }
        public vacation GetVacationByID(int? id)
        {
            return db.vacations.Find(id);
        }
        public bool AddVacations(vacation vacation)
        {
            try
            {
                db.vacations.Add(vacation);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool EditVacations(vacation vacation)
        {
            try
            {
                db.Entry(vacation).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveVacation(vacation vacation)
        {
            try
            {
                db.vacations.Remove(vacation);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        // province
        public List<province> GetProvinces()
        {
            return db.provinces.ToList();
        }
        public province GetProvinceByID(int? id)
        {
            return db.provinces.Find(id);
        }
        public bool AddProvinces(province province)
        {
            try
            {
                db.provinces.Add(province);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool EditProvinces(province province)
        {
            try
            {
                db.Entry(province).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveProvince(province province)
        {
            try
            {
                db.provinces.Remove(province);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
