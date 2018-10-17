using System;
using Microsoft.AspNetCore.Mvc;


namespace SmallSimpleOA.Controllers
{
    public class LoginController : Controller
    {
        public LoginController()
        {
        }

        public IActionResult Login()
        {

            return View();
        }
    }
}
