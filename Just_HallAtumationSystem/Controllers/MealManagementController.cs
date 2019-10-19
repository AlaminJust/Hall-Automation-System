using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using MyApp.Db.DbOperation;
using MyApp.Db;

namespace Just_HallAtumationSystem.Controllers
{
    public class MealManagementController : Controller
    {
        // GET: MealManagement
        MealManagementOperation mealManagementOperation = new MealManagementOperation();
        public ActionResult AllAcount()
        {
            var accounts = mealManagementOperation.GetAllAccount().OrderBy(x => x.room.RoomNumber);
            return View(accounts);
        }
        public ActionResult AddBalance(int? Id) // User Id
        {
            Account account = new Account();
            account.UserId = (int)Id;
            return View(account);
        }
        [HttpPost]
        public ActionResult AddBalance(Account model)
        {
            if (ModelState.IsValid)
            {
                int id = mealManagementOperation.AddBalance(model);
                if(id > 0)
                {
                    ViewBag.Success = "Balance Added!";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.Success = "Failed!";
                }
            }
            return RedirectToAction("AllAcount");
        }

        // Show Balance of Specific User
        public ActionResult ShowBalance(int? Id) // UserId
        {
            var result = mealManagementOperation.GetUserAccount((int)Id);
            return View(result);
        }
    }
}