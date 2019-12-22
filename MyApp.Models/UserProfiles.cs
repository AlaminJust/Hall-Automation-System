using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class UserProfiles
    {
        public Student student = new Student();
        public UserImage userImage = new UserImage();
        public User user = new User();
        public Address address = new Address();
        public DepartmentInfo departmentInfo = new DepartmentInfo();
        public Room room = new Room();
        public District district = new District();
        public Department department = new Department();
        public RegistrationForm registrationForm = new RegistrationForm();
    }
}
