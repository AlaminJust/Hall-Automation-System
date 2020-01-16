using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using MyApp.Db;
using MyApp.Db.DbOperation;
using System.Net;

namespace Just_HallAtumationSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        LoginRegistrationOperation loginRegistrationOperation = new LoginRegistrationOperation();
        public ActionResult Registraion()
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
        public ActionResult Registraion(AdminRegistraionModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new JustHallAtumationEntities())
                    {
                        var regPass = context.AdminRegistrionPasswords.Where(x => x.AdminRegPass.Length > 0).FirstOrDefault();
                        string RegPass = "246810";
                        if (regPass != null)
                        {
                            RegPass = regPass.AdminRegPass;
                        }
                        if (model.RegistraionPassword.Replace(" ", "") == RegPass.Replace(" ", ""))
                        {
                            var user1 = context.Users.Where(x => x.UserName.Replace(" ", "") == model.UserName.Replace(" ", "") || x.UserEmail.Replace(" ", "") == model.Email.Replace(" ", "")).FirstOrDefault();
                            if (user1 == null)
                            {
                                User user = new User()
                                {
                                    UserName = model.UserName,
                                    UserEmail = model.Email,
                                    Password = loginRegistrationOperation.HashFunction(model.Password)
                                };
                                context.Users.Add(user);
                                UsersRole usersRole = new UsersRole()
                                {
                                    Role = "Admin",
                                    UserId = user.UserId
                                };
                                context.UsersRoles.Add(usersRole);
                                context.SaveChanges();
                                ViewBag.Success = "Registraion Completed!";
                            }
                            else
                            {
                                ViewBag.Success = "User Name or User Email allready exist";
                            }
                        }
                        else
                        {
                            ViewBag.Success = "Registraion Password is not currect";
                        }

                    }
                }
                return View();


            }
            catch (Exception ex)
            {
                return View(ex);
            }



        }
        [Authorize(Roles = "Admin")]
        public ActionResult ChanegAdminRegPassword()
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult ChanegAdminRegPassword(changePasswordModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new JustHallAtumationEntities())
                    {
                        var regPass = context.AdminRegistrionPasswords.Where(x => x.AdminRegPass.Length > 0).FirstOrDefault();
                        string RegPass = "246810";
                        if (regPass != null)
                        {
                            RegPass = regPass.AdminRegPass;
                        }
                        if (model.currentPassword.Replace(" ", "") == RegPass.Replace(" ", ""))
                        {
                            if (regPass == null)
                            {
                                AdminRegistrionPassword adminRegistrionPassword = new AdminRegistrionPassword()
                                {
                                    AdminRegPass = model.newPassword
                                };
                                context.AdminRegistrionPasswords.Add(adminRegistrionPassword);
                            }
                            else
                            {
                                regPass.AdminRegPass = model.newPassword;
                            }
                            context.SaveChanges();
                            ViewBag.Success = "Succesfully Chanegd!";
                        }
                        else
                        {
                            ViewBag.Success = "Current password don't match with admin registration password!";
                        }
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex);
            }

        }
        [Authorize(Roles = "Admin,Student")]
        public ActionResult MealAdminRegistraion()
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
        [Authorize(Roles = "Admin,Student")]
        [HttpPost]
        public ActionResult MealAdminRegistraion(MealAdminReg model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new JustHallAtumationEntities())
                    {
                        var res = context.MealAdminRegs.Where(x => x.UserName.Replace(" ", "") == User.Identity.Name).FirstOrDefault();
                        if (res != null)
                        {
                            ViewBag.Success = "Already requested!";
                        }
                        else
                        {
                            MealAdminReg mealAdminReg = new MealAdminReg()
                            {
                                UserName = User.Identity.Name,
                                Name = model.Name,
                                DepartmentName = model.DepartmentName,
                                MobileNumber = model.MobileNumber,
                                Flag = 0
                            };
                            context.MealAdminRegs.Add(mealAdminReg);
                            context.SaveChanges();
                            ViewBag.Success = "Requeste successfully Submitted!";
                        }
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AllMealAdminRequest()
        {
            try
            {
                using (var context = new JustHallAtumationEntities())
                {
                    var result = context.MealAdminRegs.ToList();
                    return View(result);
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        public int UserRoleId(string UserName)
        {
            using (var context = new JustHallAtumationEntities())
            {
                var user = context.Users.Where(x => x.UserName.Replace(" ", "") == UserName.Replace(" ", "")).FirstOrDefault();
                if (user != null)
                {
                    UsersRole usersRole = new UsersRole()
                    {
                        Role = "MealAdmin",
                        UserId = user.UserId
                    };
                    context.UsersRoles.Add(usersRole);
                    context.SaveChanges();
                    return usersRole.Id;
                }
            }
            return -1;
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AcceptMealAdmin(int? Id) /// MealAdminRegsId
        {
            try
            {
                if (Id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                using (var context = new JustHallAtumationEntities())
                {
                    var result = context.MealAdminRegs.Where(x => x.MealAdminRegId == (int)(Id)).FirstOrDefault();
                    if (result != null)
                    {
                        int id = UserRoleId(result.UserName);
                        if (id == -1)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        else
                        {
                            result.Flag = 1;
                            context.SaveChanges();
                            ViewBag.Success = "Succesfull Accepted!";
                        }
                        return RedirectToAction("AllMealAdminRequest");
                    }
                    else
                    {
                        ViewBag.Success = "User is Invalid!";
                    }
                    return RedirectToAction("AllMealAdminRequest");
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ShowMealAdmins()
        {
            try
            {

                using (var context = new JustHallAtumationEntities())
                {
                    var result = context.MealAdminRegs.Where(x => x.Flag == 1).ToList();
                    return View(result);
                }

            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }


    }
}