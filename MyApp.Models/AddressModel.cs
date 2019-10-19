using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class AddressModel
    {
        public Address address { get; set; }
        public District P_district { get; set; }
        public District T_District { get; set; }
    }
}
