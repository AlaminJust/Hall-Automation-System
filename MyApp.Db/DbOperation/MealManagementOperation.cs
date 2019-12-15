using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Models;

namespace MyApp.Db.DbOperation
{
    public class MealManagementOperation
    {
        // To Get All account of users.
        public List<AllAccountsModel> GetAllAccount()
        {
            using(var context = new JustHallAtumationEntities())
            {
                var result = (from s in context.Students
                             join r in context.Rooms on s.RoomId equals r.RoomId
                             select new AllAccountsModel
                             {
                                 student = s,
                                 room = r
                             }).ToList();
                return result;
            }
        }

        // To Add Balance to the user Account
        public int AddBalance(Account model)
        {
            using (var context = new JustHallAtumationEntities())
            {
                var result = context.Accounts.Where(x => x.UserId == model.UserId).FirstOrDefault();
                if(result != null)
                {
                    result.Balance += model.Balance;
                }
                else
                {
                    return -1;
                }
                context.SaveChanges();
                return result.UserId;
            }
        }

        // To Get The Specific User Account
        public Account GetUserAccount(int UserId) // User Id
        {
            using(var context = new JustHallAtumationEntities())
            {
                var result = context.Accounts.Where(x => x.UserId == UserId).FirstOrDefault();
                return result;
            }
        }

        // To Add Meal for Specific User
        public int AddMeal(Meal model , string UserName)
        {
            using(var context = new JustHallAtumationEntities())
            {

                TimeSpan now = DateTime.Now.TimeOfDay;
                TimeSpan start = new TimeSpan(00, 0, 0);
                TimeSpan End = new TimeSpan(24, 00, 0);
                if (!(now >= start && now <= End))
                {
                    return -1;
                }

                var mealRate = context.MealCosts.FirstOrDefault();
                int MealRate = mealRate.MealCost1;
                int UserBalance = 0;
                var User = context.Users.Where(x => x.UserName == UserName).FirstOrDefault();
                var UserAccount = context.Accounts.Where(x => x.UserId == User.UserId).FirstOrDefault();
                UserBalance = (int)UserAccount.Balance;

                int TotalMealCost = (model.Dinnar * MealRate) + (model.Lunch * MealRate);
                if(TotalMealCost > UserBalance) // User has not Sufficient Balance
                {
                    return -2;
                }
                else
                {
                    UserAccount.Balance = UserAccount.Balance - TotalMealCost;
                    context.SaveChanges();
                }
                var student = context.Students.Where(x => x.UserId == User.UserId).FirstOrDefault();
                var room = context.Rooms.Where(x => x.RoomId == student.RoomId).FirstOrDefault();

                Meal meal = new Meal()
                {
                    Dinnar = model.Dinnar,
                    Lunch = model.Lunch,
                    StudentId = student.StudentId,
                    StudentName = student.StudentName,
                    RoomNo = room.RoomNumber,
                    Time = DateTime.Now.TimeOfDay,
                    Date = DateTime.Today.Date
                };

                context.Meals.Add(meal);
                context.SaveChanges();
                return meal.MealId;
            }
        }
        // To Update Meal for individual student
        public int Update(Meal Model , string UserName)
        {
            using(var context = new JustHallAtumationEntities())
            {
                TimeSpan now = DateTime.Now.TimeOfDay;
                TimeSpan start = new TimeSpan(00, 0, 0);
                TimeSpan End = new TimeSpan(24, 00, 0);
                if (!(now >= start && now <= End))
                {
                    return -1; // Time is over!
                }
                var user = context.Users.Where(x => x.UserName == UserName).FirstOrDefault();
                var student = context.Students.Where(x => x.UserId == user.UserId).FirstOrDefault();
                var meal = context.Meals.Where(x => x.Date == DateTime.Today.Date && x.StudentId == student.StudentId).FirstOrDefault(); // Find Meal from database
                if(meal == null) 
                {
                    return -2; /// First Add meal
                }
                int mealRate = context.MealCosts.FirstOrDefault().MealCost1; // To back the money 
                int backMoney = (meal.Dinnar + meal.Lunch) * mealRate; // Previous cost
                int currentCost = (Model.Lunch + Model.Dinnar) * mealRate; // Current Cost
                int UserBalance = 0;
                var User = context.Users.Where(x => x.UserName == UserName).FirstOrDefault();
                var UserAccount = context.Accounts.Where(x => x.UserId == User.UserId).FirstOrDefault();
                UserBalance = (int)UserAccount.Balance;

                if ((backMoney + UserBalance) < currentCost)
                {
                    return -3; // Insufficient balance
                }
                UserAccount.Balance = (UserBalance + backMoney - currentCost);
                meal.Dinnar = Model.Dinnar;
                meal.Lunch = Model.Lunch;
                meal.Time = DateTime.Now.TimeOfDay;
                meal.Date = DateTime.Today.Date;
                context.SaveChanges();
                return meal.MealId;
            }
        }

        // To Update Meal Rate
        public int AddMealRate(MealCost model)
        {
            using(var context = new JustHallAtumationEntities())
            {

                var result = context.MealCosts.FirstOrDefault();
                if(result == null) // Meal Cost Not Found Add Meal Cost
                {
                    MealCost mealCost = new MealCost()
                    {
                        MealCost1 = model.MealCost1
                    };
                    context.MealCosts.Add(mealCost);
                    context.SaveChanges();
                    return mealCost.MealCostId;
                }
                else // OtherWise Update MealCost
                {
                    result.MealCost1 = model.MealCost1;
                    context.SaveChanges();
                    return result.MealCostId;
                }
            }
        }
    }
}
