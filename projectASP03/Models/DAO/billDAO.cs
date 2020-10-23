using Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class billDAO
    {
        private projectASP01Entities db = new projectASP01Entities();
        // Work bill
        public List<bill> GetBills() {
            List<bill> list = new List<bill>();
            list = db.bills.ToList();
            return list;
        }

        // Work Bill Details

        public List<billDetail> GetAllBillDetails()
        {
            List<billDetail> list = new List<billDetail>();
            var res = db.billDetails.Include("bill").Include("tour");
            list = res.ToList();
            return list;
        }
        public billDetail GetBillDetailsByID (int ? id)
        {
            billDetail billDetail = db.billDetails.Find(id);
            return billDetail;
        }
        public bool AddBillDetails(billDetail billDetail)
        {
            try
            {
                db.billDetails.Add(billDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool EditBillDetails(billDetail billDetail)
        {
            try
            {
                db.Entry(billDetail).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveBillDetails(billDetail billDetail)
        {
            try
            {
                db.billDetails.Remove(billDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        // Somethings else
        public List<tour> GetTours()
        {
            List<tour> list = new List<tour>();
            list = db.tours.ToList();
            return list;
        }
    }
}
