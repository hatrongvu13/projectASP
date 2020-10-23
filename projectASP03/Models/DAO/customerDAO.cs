using Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class customerDAO
    {
        private projectASP01Entities db = new projectASP01Entities();
        public List<customer> GetCustomers()
        {
            return db.customers.ToList();
        }
        public customer GetCustomerById(int? id)
        {
            return db.customers.Find(id);
        }
        public bool AddNewCustomer(customer customer)
        {
            try
            {
                db.customers.Add(customer);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool EditCustomer(customer customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveCustomer(customer customer)
        {
            try
            {
                db.customers.Remove(customer);
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
