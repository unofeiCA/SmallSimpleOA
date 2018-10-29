using System;
using Microsoft.AspNetCore.Mvc;
using SmallSimpleOA.Models;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace SmallSimpleOA.Controllers
{
    public class LoginController : Controller
    {

        private readonly SmallSimpleOAContext _context;

        public LoginController(SmallSimpleOAContext context)
        {
            _context = context;
        }



        [HttpPost]
        public IActionResult DoLogin(string email, string password)
        {

            Uzer user = _context.Uzer.Single(e => e.Email.Equals(email));
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
                _context.Update(user);
                _context.SaveChanges();
                HttpContext.Session.SetString("uid", "" + user.Id);
                return RedirectToAction("Home", "Home");
            }else{

                return RedirectToAction("Login", "Login");

            }


        }

        public IActionResult Login()
        {

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
