using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;

namespace Just_HallAtumationSystem.Controllers
{
    public class FilterController : Controller
    {
        // GET: Filter
        public ActionResult Index()
        {
            List<District> districts = new List<District>();
            using(var context = new JustHallAtumationEntities())
            {
                districts = context.Districts.ToList();
            }
            return View(districts);
        }


    }
}