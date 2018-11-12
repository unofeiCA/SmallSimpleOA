using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace SmallSimpleOA.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Message(string id)
        {
            return View();
        }


    }
}
