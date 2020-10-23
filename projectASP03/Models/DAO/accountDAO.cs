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
    public class accountDAO
    {
        private projectASP01Entities db = new projectASP01Entities();
        public List<account> GetAllAccount()
        {
            List<account> list = db.accounts.ToList();
            return list;
        }
        public account GetAccountByID(int? id)
        {
            account ac = db.accounts.Find(id);
            return ac;
        }
        public bool AddNewAccount(account account)
        {
            try
            {
                db.accounts.Add(account);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool EditAccount(account account)
        {
            try
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
        }
        public bool RemoveAccount(account account)
        {
            try
            {
                db.accounts.Remove(account);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Login(string email, string password)
        {
            var res = db.accounts.SingleOrDefault(a => a.email.Equals(email) && a.password.Equals(password));
            if (res != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
