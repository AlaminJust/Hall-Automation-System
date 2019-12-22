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
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }
        [HttpPost]
            
        public ActionResult RegistrationForm(RegistrationFormModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = loginRegistrationOperation.RegistrationFormVerified(model);
                    if (id > 0) // Filled The Registration Form 
                    {
                        ModelState.Clear();
                        ViewBag.Success = "Registration Completed Wait For accepted By admin";
                        return RedirectToAction("EmailVerification");
                    }
                    else if (id == -1)
                    {
                        ViewBag.Success = "User Name or Email or Roll Number Already Exist!";
                    }
                    else
                    {
                        ViewBag.Success = "Email Doesn't Exist\n";
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                return View(ex);
            }
           
        }

        /// To verify Email
        
        public ActionResult EmailVerification()
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
        [HttpPost]
        public ActionResult EmailVerification(EmailVerificationModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using(var context = new JustHallAtumationEntities())
                    {
                        var registration = context.RegistrationForms.Where(x => x.UserName == model.UserName && x.Verification == model.VerificationCode && x.IsVerified == 2).FirstOrDefault();
                        if(registration != null)
                        {
                            registration.IsVerified = 0; /// Eamil Is verified Wait for 1 ( Hall Provost )
                            context.SaveChanges();
                            ViewBag.Success = "Successfully Verified Your Email\n Wait For Accepted by Hall Admin....";
                        }
                        else
                        {
                            ViewBag.Success = "User Name or Verification Code is Wrong....";
                        }
                    }
                    ModelState.Clear();
                }
                return View();
            }
            catch(Exception ex)
            {
                return View(ex);
            }
        }

        // To Registration Form Update
        public ActionResult UpdateRegistraionForm(int? Id)
        {
            try
            {
                RegistrationForm registrationForm = new RegistrationForm();
                using (var context = new JustHallAtumationEntities())
                {
                    registrationForm = context.RegistrationForms.Where(x => x.RegistrationId == (int)Id).FirstOrDefault();
                }
                return View(registrationForm);
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }
        [HttpPost]
        public ActionResult UpdateRegistraionForm(RegistrationForm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using(var context = new JustHallAtumationEntities())
                    {
                        var result = context.RegistrationForms.Where(x => x.RegistrationId == model.RegistrationId).FirstOrDefault();
                        if(result != null)
                        {
                            result.Email = model.Email;
                            result.Session = model.Session;
                            result.RollNumber = model.RollNumber;
                            result.StudentName = model.StudentName;
                            context.SaveChanges();
                            ViewBag.Success = "Email Is Updated successfully...";
                        }
                        string message = "Updated! Please Verify Your Email!\n" + "Your Verification Code is : " + model.Verification + "\n" + " User Name : " + model.UserName + "\n";
                        loginRegistrationOperation.SendEmail(model.Email, "Updated JUST Hall", message);
                    }
                    ModelState.Clear();
                }
                return View();
            }
            catch(Exception ex)
            {
                return View(ex);
            }
        }

        // To Show Registration Form For Admin
        public ActionResult ShowRegistrationForm()
        {
            try
            {
                using (var context = new JustHallAtumationEntities()) /// 0 for Email is verified 2 for Email is not Verified
                {
                    var result = context.RegistrationForms.Where(x => x.IsVerified == 0 || x.IsVerified == 2).ToList().OrderBy(x => x.IsVerified);
                    return View(result);
                }
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }

        public ActionResult ActivateTheUser(int? Id)
        {
            try
            {
                using (var context = new JustHallAtumationEntities())
                {
                    var result = context.RegistrationForms.Where(x => x.RegistrationId == (int)Id).FirstOrDefault();
                    int id = loginRegistrationOperation.Registrataion(result);
                    if (id > 0)
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
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }

       
        public ActionResult Login()
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
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = loginRegistrationOperation.Login(model);
                    if (id > 0) // login successfull
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        ViewBag.Success = "Success";
                        Session["UserName"] = model.UserName;
                       
                    }
                    else // login Failed
                    {
                        ViewBag.Success = "User Name or Password Invalid!";
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                return View(ex);
            }
           
        }
        public ActionResult Logout()
        {
            try
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                return RedirectToAction("Login", "LoginRegistration");
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }

        /// For forget password
        public ActionResult ForgetPassword()
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
        [HttpPost]
        public ActionResult ForgetPassword(string Email)
        {
            try
            {
                using (var context = new JustHallAtumationEntities())
                {
                    var result = context.Users.Where(x => x.UserEmail == Email).FirstOrDefault();
                    if (result != null) /// Email Address is exist
                    {
                        ViewBag.Success = "Your Password has been sent to your email!";
                        var resetPassword = Guid.NewGuid().ToString();
                        int EncryptedPassword = loginRegistrationOperation.HashFunction(resetPassword);
                        result.Password = EncryptedPassword;
                        context.SaveChanges();
                        loginRegistrationOperation.SendEmail(Email, "Forget Password", resetPassword);
                    }
                    else
                    {
                        ViewBag.Success = "Email is Invalid!";
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                return View(ex);
            }
          
        }

        public ActionResult ChangePassword()
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
        [HttpPost]
        public ActionResult ChangePassword(changePasswordModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new JustHallAtumationEntities())
                    {
                        string UserName = User.Identity.Name;
                        var user = context.Users.Where(x => x.UserName == UserName).FirstOrDefault();

                        if (user != null)
                        {
                            if (user.Password != loginRegistrationOperation.HashFunction(model.currentPassword))
                            {
                                ViewBag.Success = "Current Password doesn't match!";
                            }
                            else
                            {
                                ViewBag.Success = "Successfull Changed your password!";
                                user.Password = loginRegistrationOperation.HashFunction(model.newPassword);
                                context.SaveChanges();
                            }
                        }
                        else
                        {
                            ViewBag.Success = "Somethings Wrong!";
                        }
                    }
                }
                return View();

            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }

    }
}