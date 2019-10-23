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
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                int id = loginRegistrationOperation.Registrataion(model);
                if (id > 0) // Registration Successfull
                {
                    ViewBag.Success = "Congratulation! Registration Succesfull";
                    ModelState.Clear();
                }
                else // Registration Faliled
                {
                    ViewBag.Success = "User Name Already Exist";
                }
            }
            return View();
        }
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