using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class Searching
    {
        public Student student = new Student();
        public Room room = new Room();
        public User user = new User();
    }
    public class SearchingModel
    {
        public List<Searching> searchings = new List<Searching>();
        public List<Search> search = new List<Search>();
        public string searching { get; set; }
        public int SearchId { get; set; }
    }
}
