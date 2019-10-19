using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class StudentInfoModel
    {
        [Display(Name ="Room Number")]
        public int RoomId { get; set; }
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }
        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        public Nullable<int> PaymentId { get; set; }
        public List<Room> room { get; set; }
    }
    public class StudentUpdateModel : StudentInfoModel
    {
        public int StudentId { get; set; }
    }
    
    
}
