using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class StudentListModel
    {
        public User user { get; set; }
        public Student student { get; set; }
        public DepartmentInfo departmentInfo { get; set; }
        public Department department { get; set; }
        public Room room { get; set; }
        public DateTime HallEntryDate { get; set; }
    }
}
