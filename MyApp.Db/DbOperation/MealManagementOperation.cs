using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Models;

namespace MyApp.Db.DbOperation
{
    public class MealManagementOperation
    {
        // To Get All account of users.
        public List<AllAccountsModel> GetAllAccount()
        {
            using(var context = new JustHallAtumationEntities())
            {
                var result = (from s in context.Students
                             join r in context.Rooms on s.RoomId equals r.RoomId
                             select new AllAccountsModel
                             {
                                 student = s,
                                 room = r
                             }).ToList();
                return result;
            }
        }

        // To Add Balance to the user Account
        public int AddBalance(Account model)
        {
            using (var context = new JustHallAtumationEntities())
            {
                var result = context.Accounts.Where(x => x.UserId == model.UserId).FirstOrDefault();
                if(result != null)
                {
                    result.Balance += model.Balance;
                }
                else
                {
                    return -1;
                }
                context.SaveChanges();
                return result.UserId;
            }
        }

        // To Get The Specific User Account
        public Account GetUserAccount(int UserId) // User Id
        {
            using(var context = new JustHallAtumationEntities())
            {
                var result = context.Accounts.Where(x => x.UserId == UserId).FirstOrDefault();
                return result;
            }
        }
    }
}
