using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public int Registrataion(RegistrationModel model) // To registration in the site
        {
            using (var context = new JustHallAtumationEntities())
            {
                string UserName = model.UserName.Replace(" ", "");
                var user1 = context.Users.Where(x => x.UserName == UserName).FirstOrDefault();
                if (user1 != null)
                {
                    return -1; // User Name Already Exist
                }
                User user = new User()
                {
                    UserName = model.UserName,
                    UserEmail = model.UserEmail,
                    Password = HashFunction(model.Password.Replace(" ", ""))
                };
                context.Users.Add(user);
                context.SaveChanges();
                Account account = new Account // Creat account for New User
                {
                    UserId = user.UserId,
                    Balance = 0
                };
                context.Accounts.Add(account);
                context.SaveChanges();
                return user.UserId;
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
    }
}
