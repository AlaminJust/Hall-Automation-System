using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class RegistrationFormModel
    {
        public string UserName { get; set; }
        public string StudentName { get; set; }
        public string DeptName { get; set; }
        public string RollNumber { get; set; }
        public string Session { get; set; }
        public string Password { get; set; }
        public string ConfirmPassowrd { get; set; }
        public string Email { get; set; }
        public Nullable<int> IsVerified { get; set; }
    }
}
