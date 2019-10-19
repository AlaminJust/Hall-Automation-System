using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Models;

namespace MyApp.Db.DbOperation
{
    public class InsertInfoOperation
    {
        // To Get Student Id By userName info
        public int GetStudentId(string UserName)
        {
            using(var context = new JustHallAtumationEntities())
            {
                var user = context.Users.Where(x => x.UserName == UserName).FirstOrDefault();   // To get userId from UserName
                var student = context.Students.Where(x => x.UserId == user.UserId).FirstOrDefault(); // To get Student Id from UserId
                return student.StudentId;
            }
        }
        // To insert Room Info in Room Table
        public int InsertRoomInfo(Room model)
        {
            using (var context = new JustHallAtumationEntities())
            {
                Room room = new Room()
                {
                    RoomNumber = model.RoomNumber,
                    TotalSeat = model.TotalSeat,
                    EmptySeat = model.EmptySeat
                };
                context.Rooms.Add(room);
                context.SaveChanges();
                return room.RoomId;
            }
        }
        // To Insert Student Info in Student Table
        public int InsertStudentInfo(StudentInfoModel model , string UserName)
        {
            using(var context = new JustHallAtumationEntities())
            {
                var user = context.Users.Where(x => x.UserName == UserName).FirstOrDefault();
                Student student = new Student()
                {
                    StudentName = model.StudentName,
                    FatherName = model.FatherName,
                    MobileNumber = model.MobileNumber,
                    MotherName = model.MotherName,
                    RoomId = model.RoomId,
                    PaymentId = model.PaymentId,
                    UserId = user.UserId
                };
                context.Students.Add(student);
                context.SaveChanges();
                return student.StudentId;
            }
        }

        // To Insert DepartMent Name
        public int InsertDepartmentName(Department model)
        {
            using(var context = new JustHallAtumationEntities())
            {
                Department department = new Department()
                {
                    DeptName = model.DeptName
                };
                context.Departments.Add(department);
                context.SaveChanges();
                return department.DeptId;
            }
        }

        // To insert Student Department Information
        public int InsertUserDepartmentInfo(DepartmentInfoModel model, string UserName)
        {
            using (var context = new JustHallAtumationEntities())
            {
                var user = context.Users.Where(x => x.UserName == UserName).FirstOrDefault();   // To get userId from UserName
                var student = context.Students.Where(x => x.UserId == user.UserId).FirstOrDefault(); // To get Student Id from UserId
                DepartmentInfo departmentInfo = new DepartmentInfo  // One to one relationship with student to deparmentinfo table
                {
                    DepartmentId = model.DepartmentId,
                    Session = model.Session,
                    Cgpa = model.Cgpa,
                    StudentId = student.StudentId
                };
                context.DepartmentInfoes.Add(departmentInfo);
                context.SaveChanges();
                return departmentInfo.StudentId;
            }
        }
        //insert district name
        public int InsertDistrictName(District model)
        {
            using(var context=new JustHallAtumationEntities())
            {
                District district = new District()
                {
                    DistrictName = model.DistrictName
                };
                context.Districts.Add(district);
                context.SaveChanges();
                return district.DistrictId;
            }
        }
        //Insert Address
        public int InsertAddress(AddressInfoModel model ,string UserName)
        {
            using(var context=new JustHallAtumationEntities())
            {
                Address address = new Address()
                {
                    StudentId = GetStudentId(UserName),
                    P_DistrictId = model.P_DistrictId,
                    P_PostOffice = model.P_PostOffice,
                    P_VillageName = model.P_VillageName,
                    T_DistrictId = model.T_DistrictId,
                    T_PostOffice = model.T_PostOffice,
                    T_VillageName = model.T_VillageName

                };
                context.Addresses.Add(address);
                context.SaveChanges();
                return address.StudentId;

            }
        }

    }
}
