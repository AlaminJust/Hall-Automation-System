using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class RegistrationFormModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string StudentName { get; set; }
        [Required]
        public string DeptName { get; set; }
        [Required]
        public string RollNumber { get; set; }
        [Required]
        public string Session { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassowrd { get; set; }
        [Required]
        public string Email { get; set; }
        public Nullable<int> IsVerified { get; set; }
    }
}
