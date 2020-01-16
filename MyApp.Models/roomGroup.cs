using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class roomGroup
    {
        public string roomNumber { set; get; }
        public Student student { set; get; }
        public int totolSeat { set; get; }
    }
}
