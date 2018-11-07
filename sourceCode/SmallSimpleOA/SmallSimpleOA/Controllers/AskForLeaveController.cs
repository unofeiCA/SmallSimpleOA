﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallSimpleOA.ViewModels;
using SmallSimpleOA.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmallSimpleOA.Controllers
{
    public class AskForLeaveController : Controller
    {
        // GET: /<controller>/
        public IActionResult List()
        {
            return View();
        }

        public IActionResult New(string r)
        {
            AskFroLeaveNewViewModel askFroLeaveNewViewModel = new AskFroLeaveNewViewModel();

            if (r == null)
            {
                askFroLeaveNewViewModel.LastNewSuccess = 0;

            }
            else if (r.Equals("0"))
            {
                askFroLeaveNewViewModel.LastNewSuccess = 1;
                askFroLeaveNewViewModel.LastNewPrompt = "You have applied a request of leave, please wait for your supervisors to review.";
            }
            else
            {
                askFroLeaveNewViewModel.LastNewSuccess = -1;
                askFroLeaveNewViewModel.LastNewPrompt = r;
            }
            return View(askFroLeaveNewViewModel);

        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult DoNew(string startTime, string endTime, string reason, string memo)
        {

            string result = "0";
            if (startTime == null ||
               endTime == null ||
               reason == null ||
               startTime.Trim().Equals("") ||
               endTime.Trim().Equals("") ||
               reason.Trim().Equals("") 
              )
            {
                result = "Fields of 'From', 'To' and 'Reason' should be filled.";
                return RedirectToAction("New", "AskForLeave", new { r = result });
            }

            int? uid = HttpContext.Session.GetInt32("uid");

            DateTime st, et;
            if(!DateTime.TryParse(startTime, out st) || !DateTime.TryParse(endTime, out et))
            {
                result = "Time format not valid.";
                return RedirectToAction("New", "AskForLeave", new { r = result });
            }
            if(uid == null)
            {
                return RedirectToAction("Login", "Login");

            }

            AskForLeaveService.AddAskForLeave((int)uid, st, et, DateTime.Now, reason, memo);

            return RedirectToAction("New", "AskForLeave", new { r = result });
        }
    }
}
