using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class EmailVerificationModel
    {
        public string UserName { get; set; }
        public string VerificationCode { get; set; }
    }
}
