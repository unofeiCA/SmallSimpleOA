using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using SmallSimpleOA.Services;
using SmallSimpleOA.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmallSimpleOA.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: /<controller>/
        public IActionResult History()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DoClock(string actionType, string by = "ajax")
        {

            int? uid = HttpContext.Session.GetInt32("uid");
            if (uid == null)
            {
                return RedirectToAction("Login", "Login");
            }

            string result = "0";
            if (actionType == null || actionType.Trim().Equals(""))
            {
                result = "Some error occured. Please try again later.";
            }

            int acType;
            if (!Int32.TryParse(actionType, out acType) || acType < (int)AttendanceType.In || acType > (int)AttendanceType.Out)
            {
                result = "Some error occured. Please try again later.";
            }

            if (result.Equals("0"))
            {
                if (acType == (int)AttendanceType.In)
                {
                    List<Attendance> clockIns = AttendanceService.FindAttendanceByUserAndDateAndType((int)uid, DateTime.Now, AttendanceType.In);
                    if (clockIns.Count > 0)
                    {
                        Attendance clockIn = clockIns[clockIns.Count - 1];
                        result = "You have clocked in today at ." + clockIn.ActionTime.ToString();
                    }
                    else
                    {
                        AttendanceService.AddAttendance((int)uid, DateTime.Now, (AttendanceType)acType);
                    }
                }
                else
                {
                    DateTime t = DateTime.Now;
                    List<Attendance> clockOuts = AttendanceService.FindAttendanceByUserAndDateAndType((int)uid, t, AttendanceType.In);
                    if (clockOuts.Count > 0)
                    {
                        Attendance clkOut = clockOuts[clockOuts.Count - 1];
                        clkOut.ActionTime = t;
                        AttendanceService.UpdateAttendance(clkOut);
                    }
                    else
                    {
                        AttendanceService.AddAttendance((int)uid, t, (AttendanceType)acType);
                    }
                }
            }

            if (by.Equals("ajax"))
            {
                return Content(result);
            }
            else
            {
                return RedirectToAction("History", "Attendance", new { r = result });
            }

        }
    }
}

