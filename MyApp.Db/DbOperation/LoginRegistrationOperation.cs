using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using MyApp.Models;

namespace MyApp.Db.DbOperation
{
    public class LoginRegistrationOperation
    {
        public int HashFunction(string str)
        {
            long Base = 127, Mod = 1000000007;
            long ans = 0;
            int len = str.Length;
            long pow = 1;
            for (int i = 0; i < len; i++)
            {
                ans = (ans + str[i] * pow) % Mod;
                pow = (pow * Base) % Mod;
            }
            return (int)ans;
        }

        // For Registration Form to verified the user
        public int RegistrationFormVerified(RegistrationFormModel model)
        {
           
            using(var context = new JustHallAtumationEntities())
            {

                var user1 = context.RegistrationForms.Where(x => x.UserName == model.UserName.Replace(" ","") || x.Email == model.Email || x.RollNumber == model.RollNumber).FirstOrDefault();
                if (user1 != null)
                {
                    return -1; // User Name or Email or Roll Number Already Exist
                }
                RegistrationForm registrationForm = new RegistrationForm
                {
                    UserName = model.UserName,
                    StudentName = model.StudentName,
                    DeptName = model.DeptName,
                    Email = model.Email,
                    Session = model.Session,
                    IsVerified = 2, ///still Email Not verified 
                    Password = HashFunction(model.Password.Replace(" ", "")),
                    RollNumber = model.RollNumber,
                    Verification = Guid.NewGuid().ToString()
                };

                string message = "Please Verify Your Email!\n" + "Your Verification Code is : " + registrationForm.Verification + "\n" + " User Name : " + registrationForm.UserName +"\n";

                if(SendEmail(model.Email.Replace(" ","") , "Registration Just Hall" , message))
                {
                    context.RegistrationForms.Add(registrationForm);
                    context.SaveChanges();
                    return registrationForm.RegistrationId;
                }
                else
                {
                    return -2; /// Email doesn't Exist.
                }
            }
        }
        public int Registrataion(RegistrationForm model) // To registration in the site
        {
            string Subject = "Activation";
            string Message = "Your Account Successfully created!";

            using (var context = new JustHallAtumationEntities())
            {
                User user = new User()
                {
                    UserName = model.UserName,
                    UserEmail = model.Email,
                    Password = model.Password
                };
                context.Users.Add(user);
                context.SaveChanges();
                Account account = new Account // Creat account for New User
                {
                    UserId = user.UserId,
                    Balance = 0
                };
                if (SendEmail(model.Email.Replace(" ", ""), Subject, Message))
                {
                    context.Accounts.Add(account);
                    context.SaveChanges();
                    return user.UserId;
                }
                else return -1; // Email address is not currect
            }
        }
        public int Login(LoginModel model) // To login in the site
        {
            using (var context = new JustHallAtumationEntities())
            {
                string UserName = model.UserName.Replace(" ", "");
                string Password = model.Password.Replace(" ", "");
                int HashPassword = HashFunction(Password);
                var user = context.Users.Where(x => x.UserName == UserName && x.Password == HashPassword).FirstOrDefault();
                if (user != null) // If user registrared 
                {
                    return user.UserId;
                }
                else return -1; // If user not registrated
            }
        }


        // Email sending function
        public bool SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (receiver != null)
                {
                    var senderEmail = "alamin.cse.just@gmail.com";
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "Alamin1@#$";
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
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
    }
}
