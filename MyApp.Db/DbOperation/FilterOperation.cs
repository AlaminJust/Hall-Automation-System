using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Models;

namespace MyApp.Db.DbOperation
{
    public class FilterOperation
    {
        // Filter By Parmanent District
        public List<AllAccountsModel> GetSpecificDistrictAllStudent(int DistricId) // District Id
        {
            using(var context = new JustHallAtumationEntities())
            {
                var result = (from s in context.Students
                              join a in context.Addresses on s.StudentId equals a.StudentId
                              join r in context.Rooms on s.RoomId equals r.RoomId
                              where (a.P_DistrictId == DistricId)
                              select new AllAccountsModel
                              {
                                  student = s,
                                  room = r
                              }).ToList();
                return result;
            }
        }
    }
}
