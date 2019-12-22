using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Models;

namespace MyApp.Db.DbOperation
{
    public class SearchOperation
    {
        // TO get Student table data by UserName
        InsertInfoOperation insertInfoOperation = new InsertInfoOperation();
        public List<UserProfiles> GetStudentInformation(string UserName)
        {
            using(var context = new JustHallAtumationEntities())
            {
                var profiles = (from u in context.Users
                                join s in context.Students on u.UserId equals s.UserId
                                join r in context.Rooms on s.RoomId equals r.RoomId
                                join depInfo in context.DepartmentInfoes on s.StudentId equals depInfo.StudentId
                                join d in context.Departments on depInfo.DepartmentId equals d.DeptId
                                join add in context.Addresses on s.StudentId equals add.StudentId
                                join dis in context.Districts on add.P_DistrictId equals dis.DistrictId
                                join img in context.UserImages on u.UserId equals img.UserId
                                where (u.UserName == UserName)
                                select new UserProfiles
                                {
                                    student = s,
                                    userImage = img,
                                    user = u,
                                    room = r,
                                    departmentInfo = depInfo,
                                    district = dis,
                                    department = d,
                                    address = add
                                }).ToList();

                if(profiles.Count() > 0 )
                    profiles[0].registrationForm = context.RegistrationForms.Where(x => x.UserName == UserName).FirstOrDefault();
                return profiles;
            }
        }

        public List<StudentListModel> GetStudentList()
        {
            using(var context = new JustHallAtumationEntities())
            {
                var students = (from u in context.Users
                               join s in context.Students on u.UserId equals s.UserId
                               join r in context.Rooms on s.RoomId equals r.RoomId
                               join d in context.DepartmentInfoes on s.StudentId equals d.StudentId
                               join dep in context.Departments on d.DepartmentId equals dep.DeptId
                               select new StudentListModel
                               {
                                   student = s,
                                   user = u,
                                   departmentInfo = d,
                                   room = r,
                                   department = dep,
                                   HallEntryDate = DateTime.Now
                               }).ToList();
                return students;
            }
        }

        // To get Address by StudentId
        public AddressModel GetAdressOfUser(int StudentId)
        {
            using(var context = new JustHallAtumationEntities())
            {
                var result = (from a in context.Addresses
                             join d1 in context.Districts on a.P_DistrictId equals d1.DistrictId
                             join d2 in context.Districts on a.T_DistrictId equals d2.DistrictId
                             where (a.StudentId == StudentId)
                             select new AddressModel
                             {
                                 address = a,
                                 P_district = d1,
                                 T_District = d2
                             }).FirstOrDefault();
                return result;
            }
        }

        // To Get Room of User by StudentId
        public Room GetRoomOfUser(int StudentId)
        {
            using (var context = new JustHallAtumationEntities())
            {
                var student = context.Students.Where(x => x.StudentId == StudentId).FirstOrDefault();
                var result = context.Rooms.Where(x => x.RoomId == student.RoomId).FirstOrDefault();
                return result;
            }
        }

        // To Get Department Information by Student Id
        public DepartMentWithDeptName GetDepartmentInfoOfUser(int StudentId)
        {
            using (var context = new JustHallAtumationEntities())
            {
                var result = (from d in context.DepartmentInfoes
                              join dep in context.Departments on d.DepartmentId equals dep.DeptId
                              where (d.StudentId == StudentId)
                              select new DepartMentWithDeptName
                              {
                                  Session = d.Session,
                                  Cgpa = d.Cgpa,
                                  DepartmentName = dep.DeptName
                              }).FirstOrDefault();
                return result;
            }
        }


    }
}
