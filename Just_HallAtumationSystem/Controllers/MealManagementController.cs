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
        [Authorize(Roles ="Admin")]
        public ActionResult AllAcount()
        {
            var accounts = mealManagementOperation.GetAllAccount().OrderBy(x => x.room.RoomNumber);
            return View(accounts);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddBalance(int? Id) // User Id
        {
            Account account = new Account();
            account.UserId = (int)Id;
            return View(account);
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]

        // Show Balance of Specific User
        public ActionResult ShowBalance(int? Id) // UserId
        {
            var result = mealManagementOperation.GetUserAccount((int)Id);
            return View(result);
        }

        public ActionResult AddMeal() // User Id
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddMeal(Meal model)
        {
            string UserName = User.Identity.Name;
            if (ModelState.IsValid)
            {
                int id = mealManagementOperation.AddMeal(model, UserName);
            }
            return View();
        }

        // To Update Meal
        public ActionResult UpdateMeal()
        {
            string UserName = User.Identity.Name;
            Meal meal = new Meal();
            using(var context = new JustHallAtumationEntities())
            {
                var user = context.Users.Where(x => x.UserName == UserName).FirstOrDefault();
                var student = context.Students.Where(x => x.UserId == user.UserId).FirstOrDefault();
                meal = context.Meals.Where(x => x.Date == DateTime.Today.Date && x.StudentId == student.StudentId).FirstOrDefault();
            }
            
            return View(meal);
        }
        [HttpPost]
        public ActionResult UpdateMeal(Meal Model)
        {
            if (ModelState.IsValid)
            {
                int id = mealManagementOperation.Update(Model, User.Identity.Name);
                if(id == -1)
                {
                    ViewBag.Success = "Time is Over!";
                }
                else if(id == -2)
                {
                    ViewBag.Success = "You have no Meal Yet!";
                }
                else if(id == -3)
                {
                    ViewBag.Success = "Insufficient Balance!";
                }
                else if(id > 0)
                {
                    ViewBag.Success = "Succesfully Updated!";
                }
                else
                {
                    ViewBag.Success = "Somethings Went Wrong!";
                }
            }
            return View();
        }

        // To Add MealRate
        public ActionResult AddMealRate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddMealRate(MealCost model)
        {
            if (ModelState.IsValid)
            {
                int id = mealManagementOperation.AddMealRate(model);
                if(id > 0)
                {
                    ModelState.Clear();
                    ViewBag.Success = "Meal Rate Added!";
                }
                else
                {
                    ViewBag.Success = "Meal Rate Not Added!";
                }
            }
            return RedirectToAction("ShowMealRate");
        }
        // To MealRate
        public ActionResult ShowMealRate()
        {
            using(var context = new JustHallAtumationEntities())
            {
                var result = context.MealCosts.FirstOrDefault();
                return View(result);
            }
        }

        // Show Meal Today MealList

        public ActionResult ShowTodayAllMeal()
        {
            using (var context = new JustHallAtumationEntities())
            {
                var result = context.Meals.Where(x => x.Date == DateTime.Today.Date).ToList();
                var sortedResult = result.OrderBy(x => x.RoomNo).ToList();
                return View(sortedResult);
            }
        }
        // To Show Previous Day MealList
        public ActionResult ShowPreviousDayMeal()
        {
            using (var context = new JustHallAtumationEntities())
            {
                var result = context.Meals.Where(x => x.Date == DateTime.Today.Date.AddDays(-1)).ToList();
                var sortedResult = result.OrderBy(x => x.RoomNo).ToList();
                return View(sortedResult);
            }
        }
    }
}