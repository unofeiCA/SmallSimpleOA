using System;
using Microsoft.AspNetCore.Mvc;
using SmallSimpleOA.Models;
using System.Text;
using Microsoft.AspNetCore.Http;
using SmallSimpleOA.Services;
using Microsoft.AspNetCore.Http.Extensions;
using SmallSimpleOA.Utilities;

namespace SmallSimpleOA.Controllers
{
    public class LoginController : Controller
    {
   
        public LoginController(){}


        [HttpPost]
        public IActionResult DoLogin(string email, string password)
        {
            //HttpContext.Session.SetInt32("uid", 2007);
            //return RedirectToAction("Home", "Home");

            if (email == null || password == null)
            {
                return RedirectToAction("Login", "Login", new { pwdNotCorrect = "1" });
            }

            Uzer user = UserService.FindUserByEmail(email);

            string hash = MD5Util.MD5Value(password + user.Salt);
            if (hash.Equals(user.Password))
            {
                string salt = UserPasswordUtil.GenerateSalt();
                user.Salt = salt;
                string newPwd = UserPasswordUtil.GeneratePasswordAfterSalt(password, salt);
                user.Password = newPwd;
                UserService.UpdateUser(user);
                HttpContext.Session.SetInt32("uid", user.Id);
                return RedirectToAction("Home", "Home");
            }else{
                return RedirectToAction("Login", "Login", new {pwdNotCorrect = "1"});

            }


        }

        public IActionResult Login(string pwdNotCorrect)
        {
            if(pwdNotCorrect != null && pwdNotCorrect.Equals("1"))
            {
                ViewData["pwdNotCorrect"] = "1";
            }
            return View();
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Remove("uid");

            return RedirectToAction("Login", "Login");
        }

    }
}
