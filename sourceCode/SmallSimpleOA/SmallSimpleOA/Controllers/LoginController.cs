using System;
using Microsoft.AspNetCore.Mvc;
using SmallSimpleOA.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using SmallSimpleOA.Services;
using Microsoft.AspNetCore.Http.Extensions;

namespace SmallSimpleOA.Controllers
{
    public class LoginController : Controller
    {

        //private readonly SmallSimpleOAContext _context;

        //public LoginController(SmallSimpleOAContext context)
        //{
        //    _context = context;
        //}
        public LoginController(){}


        [HttpPost]
        public IActionResult DoLogin(string email, string password)
        {
            HttpContext.Session.SetInt32("uid", 1);
            return RedirectToAction("Home", "Home");

            if (email == null || password == null)
            {
                return RedirectToAction("Login", "Login", new { pwdNotCorrect = "1" });
            }

            Uzer user = UserService.FindUserByEmail(email);

            string hash = MD5Value(password + user.Salt);
            if (hash.Equals(user.Password))
            {
                Random rnd = new Random();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 4; i ++)
                {
                    int r = rnd.Next(97, 123); // from a ~ z in ascii
                    sb.Append((char)r);
                }
                string salt = sb.ToString();
                user.Salt = salt;
                string newPwd = MD5Value(password + salt);
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


        private string MD5Value(string s)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] md5Bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < md5Bytes.Length; i++)
            {
                sBuilder.Append(md5Bytes[i].ToString("x2"));
            }
            string res = sBuilder.ToString();
            return res;
        }
    }
}
