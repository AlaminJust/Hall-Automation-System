using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using MyApp.Db.DbOperation;
using System.Web.Security;

namespace Just_HallAtumationSystem.Controllers
{
    public class LoginRegistrationController : Controller
    {
        // GET: LoginRegistration
        LoginRegistrationOperation loginRegistrationOperation = new LoginRegistrationOperation();

        // For Verified the User
        public ActionResult RegistrationForm()
        {
            return View();
        }
        [HttpPost]
            
        public ActionResult RegistrationForm(RegistrationFormModel model)
        {
            if (ModelState.IsValid)
            {
                int id = loginRegistrationOperation.RegistrationFormVerified(model);
                if(id > 0) // Filled The Registration Form 
                {
                    ModelState.Clear();
                    ViewBag.Success = "Registration Completed Wait For accepted By admin";
                }
                else
                {
                    ViewBag.Success = "Failed!";
                }
            }
            return View();
        }

        // To Show Registration Form For Admin
        public ActionResult ShowRegistrationForm()
        {
            using(var context = new JustHallAtumationEntities())
            {
                var result = context.RegistrationForms.ToList().OrderBy(x=>x.IsVerified);
                return View(result);
            }
        }

        public ActionResult ActivateTheUser(int? Id)
        {
            using(var context = new JustHallAtumationEntities())
            {
                var result = context.RegistrationForms.Where(x => x.RegistrationId == (int)Id).FirstOrDefault();
                int id = loginRegistrationOperation.Registrataion(result);
                if(id > 0)
                {
                    ViewBag.Success = "Successfully Activated The User!";
                    result.IsVerified = 1;
                    context.SaveChanges();
                }
                else
                {
                    ViewBag.Success = "Failed to Verified The user!";
                }
                return RedirectToAction("ShowRegistrationForm");
            }
        }

        //public ActionResult Registration()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Registration(RegistrationModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int id = loginRegistrationOperation.Registrataion(model);
        //        if (id > 0) // Registration Successfull
        //        {
        //            ViewBag.Success = "Congratulation! Registration Succesfull";
        //            ModelState.Clear();
        //        }
        //        else // Registration Faliled
        //        {
        //            ViewBag.Success = "User Name Already Exist";
        //        }
        //    }
        //    return View();
        //}
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                int id = loginRegistrationOperation.Login(model);
                if (id > 0) // login successfull
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    ViewBag.Success = "Success";
                    Session["UserName"] = model.UserName;
                    return RedirectToAction("Index", "Home");
                }
                else // login Failed
                {
                    ViewBag.Success = "User Name or Password Invalid!";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "LoginRegistration");
        }


    }
}