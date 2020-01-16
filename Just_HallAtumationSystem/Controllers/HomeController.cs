using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Just_HallAtumationSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        JustHallAtumationEntities context = new JustHallAtumationEntities();
        [Authorize(Roles = "Admin,Student,MealAdmin")]
        public ActionResult Index()
        {
            return RedirectToAction("Notices");
        }
        [Authorize(Roles = "Admin,Student,MealAdmin")]
        public ActionResult Notices()
        {
            try
            {
                using (var context = new JustHallAtumationEntities())
                {
                    NoticeWithRegistrationForm noticeWithRegistrationForm = new NoticeWithRegistrationForm();
                    noticeWithRegistrationForm.files = context.Files.ToList();
                    noticeWithRegistrationForm.registrationForms = context.RegistrationForms.Where(x => x.IsVerified == 0 || x.IsVerified == 2).ToList().OrderBy(x => x.IsVerified).ToList();
                    return View(noticeWithRegistrationForm);
                }
            }
            catch(Exception ex)
            {
                return View(ex);
            }
        }

        /// To search Everythings
        [Authorize(Roles = "Admin,Student,MealAdmin")]
        public ActionResult Search(string Id)
        {
            try
            {
                if (Id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    return View(context.Students.Where(x => x.StudentName.Contains(Id) || (x.Room.RoomNumber.ToString().Contains(Id)) || x.User.UserName.Contains(Id) || Id == null).ToList());
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            
        }

        [Authorize(Roles = "Student")]
        public ActionResult About()
        {
            try
            {
                ViewBag.Message = "Your application description page.";

                return View();
            }
            catch(Exception ex)
            {
                return View(ex);
            }
        }
        [Authorize(Roles = "Admin,Student,MealAdmin")]
        public ActionResult Contact()
        {
            try
            {
                ViewBag.Success = "Your Information Allready added if you find any wrong informatoin go to the Hall admin";
                return View();
            }
            catch(Exception ex)
            {
                return View(ex);
            }
        }
        [Authorize(Roles = "Admin,Student,MealAdmin")]
        public ActionResult SendEmail()
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
        [Authorize(Roles = "Admin,Student,MealAdmin")]
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = System.Configuration.ConfigurationManager.AppSettings["EmailPassword"].ToString();
                    var sub = subject;
                    var body = message;
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    client.Timeout = 100000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(senderEmail, password);
                    MailMessage mailMessage = new MailMessage(senderEmail, receiver, sub, body);
                    mailMessage.IsBodyHtml = true;
                    mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                    client.Send(mailMessage);
                    ViewBag.Error = "Successfully Send Email";
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }
            return View();
        }
    }
}