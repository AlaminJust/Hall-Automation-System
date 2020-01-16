using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class AdminRegistraionModel
    {
        [Required]
        public string UserName { set; get; }
        [Required]
        public string Email { set; get; }
        [Required]
        public string Password { set; get; }
        [Compare("Password")]
        [Required(ErrorMessage = "Password Doesn't match")]
        public string ConfirmPassword { set; get; }
        [Required]
        public string RegistraionPassword { set; get; }

    }
}
