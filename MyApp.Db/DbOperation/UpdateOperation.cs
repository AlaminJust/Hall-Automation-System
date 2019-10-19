using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Models;

namespace MyApp.Db.DbOperation
{
    public class UpdateOperation
    {
        // For updating StudnetInformation 
        public int StudentInformationUpdate(StudentUpdateModel model)
        {
            using(var context = new JustHallAtumationEntities())
            {
                var student = context.Students.Where(x => x.StudentId == model.StudentId).FirstOrDefault();
                if(student != null)
                {
                    student.StudentName = model.StudentName;
                    student.RoomId = model.RoomId;
                    student.FatherName = model.FatherName;
                    student.MotherName = model.MotherName;
                    student.MobileNumber = model.MobileNumber;
                    student.PaymentId = null;
                }
                else
                {
                    return -1;
                }
                context.SaveChanges();
                return student.StudentId;
            }
        }

        //For updating student address
        public int AddressInformationUpdate(AddressInfoModel model)
        {
            using(var context=new JustHallAtumationEntities())
            {
                var address = context.Addresses.Where(x => x.StudentId == model.StudentId).FirstOrDefault();
                if (address != null)
                {
                    address.P_DistrictId = model.P_DistrictId;
                    address.P_PostOffice = model.P_PostOffice;
                    address.P_VillageName = model.P_VillageName;
                    address.T_DistrictId = model.T_DistrictId;
                    address.T_PostOffice = model.T_PostOffice;
                    address.T_VillageName = model.T_VillageName;
                }
                else
                {
                    return -1;
                }
                context.SaveChanges();
                return address.StudentId;
            }
        }


    }
}
