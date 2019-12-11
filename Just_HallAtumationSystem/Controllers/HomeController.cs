using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Just_HallAtumationSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        JustHallAtumationEntities context = new JustHallAtumationEntities();
        public ActionResult Index(string searching)
        {
            return View(context.Students.Where(x => x.StudentName.Contains(searching) || (x.Room.RoomNumber.ToString().Contains(searching)) || x.User.UserName.Contains(searching) || searching == null).ToList());
        }




        public ActionResult search()
        {
            return View();
        }
        [Authorize(Roles = "Student")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize(Roles ="Admin")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}