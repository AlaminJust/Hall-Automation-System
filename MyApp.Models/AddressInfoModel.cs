using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class AddressInfoModel
    {
        public int StudentId { get; set; }
        [Display(Name="Parmanent District Name")]
        public int P_DistrictId { get; set; }
        [Display(Name = "Parmanent Village Name")]
        public string P_VillageName { get; set; }
        [Display(Name = "Parmanent PostOffice Name")]
        public string P_PostOffice { get; set; }
        [Display(Name = "Temporary District Name")]
        public int T_DistrictId { get; set; }
        [Display(Name = "Temporary Village Name")]
        public string T_VillageName { get; set; }
        [Display(Name = "Temporary PostOffice Name")]
        public string T_PostOffice { get; set; }
        public List<District> districts { get; set; }
    }
}
