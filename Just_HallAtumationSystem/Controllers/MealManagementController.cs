using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using MyApp.Db.DbOperation;
using MyApp.Db;
using static System.Net.WebRequestMethods;
using System.Net;

namespace Just_HallAtumationSystem.Controllers
{
    public class MealManagementController : Controller
    {
        // GET: MealManagement
        MealManagementOperation mealManagementOperation = new MealManagementOperation();
        [Authorize(Roles ="MealAdmin")]
        public ActionResult AllAcount()
        {
            try
            {
                var accounts = mealManagementOperation.GetAllAccount().OrderBy(x => x.room.RoomNumber);
                return View(accounts);
            }
            catch(Exception ex)
            {
                return View(ex);
            }
           
        }
        [Authorize(Roles = "MealAdmin")]
        public ActionResult AddBalance(int? Id) // User Id
        {
            try
            {
                if (Id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Account account = new Account();
                account.UserId = (int)Id;
                return View(account);
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }
        [Authorize(Roles = "MealAdmin")]
        [HttpPost]
        public ActionResult AddBalance(Account model)
        {
            if (ModelState.IsValid)
            {
                int id = mealManagementOperation.AddBalance(model, User.Identity.Name);
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
        // [Authorize(Roles = "Admin")]

        // Show Balance of Specific User
        [Authorize(Roles = "Admin,Student,MealAdmin")]
        public ActionResult ShowBalance(int? Id) // UserId
        {
            try
            {
                if (Id == null)
                {
                    using (var context = new JustHallAtumationEntities())
                    {
                        var user = context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                        Id = user.UserId;
                    }
                }
                if (Id == null)
                {
                     return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var result = mealManagementOperation.GetUserAccount((int)Id);
                return View(result);
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }
        [Authorize(Roles = "Admin,Student,MealAdmin")]
        public ActionResult AddMeal() // User Id
        {
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                return View(ex);
            }
        }
        [Authorize(Roles = "Admin,Student,MealAdmin")]
        [HttpPost]
        public ActionResult AddMeal(Meal model)
        {
            try
            {
                string UserName = User.Identity.Name;
                if (ModelState.IsValid)
                {
                    int id = mealManagementOperation.AddMeal(model, UserName);
                    if(id == -1)
                    {
                        ViewBag.Success = "Time is Over!";
                    }
                    else if(id == -2)
                    {
                        ViewBag.Success = "Insufficient Balance!";
                    }
                    else if(id == -3)
                    {
                        ViewBag.Success = "Meal Already has been given. You can update it!";
                    }
                    else if(id == -5)
                    {
                        ViewBag.Success = "All Information must feel up before add the meals!";
                    }
                    else if(id > 0)
                    {
                        ViewBag.Success = "Meal successfully given!";
                    }
                    else
                    {
                        ViewBag.Success = "Somethings Wrong Report it!";
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }

        // To Update Meal
        [Authorize(Roles = "Admin,Student,MealAdmin")]
        public ActionResult UpdateMeal()
        {
            try
            {
                string UserName = User.Identity.Name;
                Meal meal = new Meal();
                using (var context = new JustHallAtumationEntities())
                {
                    var user = context.Users.Where(x => x.UserName == UserName).FirstOrDefault();
                    var student = context.Students.Where(x => x.UserId == user.UserId).FirstOrDefault();
                    if(student == null)
                    {
                        ViewBag.Success = "All Information must feel before add the meals!";
                        return View();
                    }
                    meal = context.Meals.Where(x => x.Date == DateTime.Today.Date && x.StudentId == student.StudentId).FirstOrDefault();
                }

                return View(meal);
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Student,MealAdmin")]
        public ActionResult UpdateMeal(Meal Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = mealManagementOperation.Update(Model, User.Identity.Name);
                    if (id == -1)
                    {
                        ViewBag.Success = "Time is Over!";
                    }
                    else if (id == -2)
                    {
                        ViewBag.Success = "You have no Meal Yet!";
                    }
                    else if (id == -3)
                    {
                        ViewBag.Success = "Insufficient Balance!";
                    }
                    else if(id == -5)
                    {
                        ViewBag.Success = "Sorry, Still your all information didn't fill up. Plaease fill up your information!";
                    }
                    else if (id > 0)
                    {
                        ViewBag.Success = "Succesfully Updated!";
                    }
                    else
                    {
                        ViewBag.Success = "Somethings Went Wrong! Report It";
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }

        // To Add MealRate
        [Authorize(Roles = "MealAdmin")]
        public ActionResult AddMealRate()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            
        }
        [HttpPost]
        [Authorize(Roles = "MealAdmin")]
        public ActionResult AddMealRate(MealCost model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = mealManagementOperation.AddMealRate(model);
                    if (id > 0)
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
            catch (Exception ex)
            {
                return View(ex);
            }
            
        }
        // To MealRate
        [Authorize(Roles = "Admin,Student,MealAdmin")]
        public ActionResult ShowMealRate()
        {
            try
            {
                using (var context = new JustHallAtumationEntities())
                {
                    var result = context.MealCosts.FirstOrDefault();
                    return View(result);
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            
        }
        public ActionResult ShowTransaction()
        {
            try
            {
                using(var context = new JustHallAtumationEntities())
                {
                    var user = context.Users.Where(x => x.UserName.Replace(" ", "") == User.Identity.Name.Replace(" ", "")).FirstOrDefault();
                    if(user == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    else
                    {
                        var result = context.Transactions.Where(x => x.UserId == user.UserId).ToList();
                        return View(result);
                    }
                    
                }
                

            }catch(Exception ex)
            {
                return View(ex);
            }
        }
        // Show Meal Today MealList
        [Authorize(Roles = "Admin,Student,MealAdmin")]
        public ActionResult ShowTodayAllMeal()
        {
            try
            {
                using (var context = new JustHallAtumationEntities())
                {
                    var result = context.Meals.Where(x => x.Date == DateTime.Today.Date).ToList();
                    var sortedResult = result.OrderBy(x => x.RoomNo).ToList();
                    return View(sortedResult);
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            
        }
        // To Show Previous Day MealList
        public ActionResult ShowPreviousDayMeal()
        {
            try
            {
                using (var context = new JustHallAtumationEntities())
                {
                    var result = context.Meals.Where(x => x.Date == DateTime.Today.Date.AddDays(-1)).ToList();
                    var sortedResult = result.OrderBy(x => x.RoomNo).ToList();
                    return View(sortedResult);
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            
        }
    }
}