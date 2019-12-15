using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class DepartmentInfoModel
    {
        [Display(Name ="Department Name")]
        public int DepartmentId { get; set; }
        [Display(Name = "Department Session")]
        public string Session { get; set; }
        [Display(Name = "Total CGPA")]
        public string Cgpa { get; set; }
        public List<Department> department { get; set; }
    }
    public class DepartMentWithDeptName : DepartmentInfoModel
    {
        public string DepartmentName { get; set; }
        public int StudentId { get; set; }
        
    }
   
}
