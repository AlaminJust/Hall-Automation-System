using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class changePasswordModel
    {
        [Required]
        public string currentPassword { get; set; }
        [Required]
        public string newPassword { get; set; }
        [Required][Compare("newPassword" , ErrorMessage ="Password and Confirm Password don't match!")]
        public string confirmPassword { get; set; }
    }
}
