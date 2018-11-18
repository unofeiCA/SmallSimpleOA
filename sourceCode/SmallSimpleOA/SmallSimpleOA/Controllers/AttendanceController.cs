using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using SmallSimpleOA.Services;
using SmallSimpleOA.Models;
using SmallSimpleOA.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmallSimpleOA.Controllers
{
    public class AttendanceController : Controller
    {

        public IActionResult GetHistory(string start, string end, string uid)
        {
            int u;
            DateTime startTime, endTime;
            if (start == null 
                || end == null 
                || uid == null 
                || !Int32.TryParse(uid, out u) 
                || !DateTime.TryParse(start, out startTime)
                || !DateTime.TryParse(end, out endTime)
                || u <= 0)
            {
                return Content("");
            }

            List<Attendance> attendances = AttendanceService.FindAttendanceByUserWithinDates(u, startTime, endTime);
            string attendanceJson = "";
            attendanceJson += "[";
            for (int i = 0; i < attendances.Count; i++)
            {
                Attendance attendance = attendances[i];
                if (i == 0)
                {
                    attendanceJson += "{";
                }
                else
                {
                    attendanceJson += ",{";
                }

                double timeStamp = ((DateTime)attendance.ActionTime).Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                timeStamp *= 1000;
                
                //DateTime time = (DateTime)attendance.ActionTime;
                //string timeStamp = time.Year + "-";
                //timeStamp += time.Month < 10 ? "0" + time.Month : "" + time.Month;
                //timeStamp += "-";
                //timeStamp += time.Day < 10 ? "0" + time.Day : "" + time.Day;
                //timeStamp += "T";
                //timeStamp += time.Hour < 10 ? "0" + time.Hour : "" + time.Hour;
                //timeStamp += ":";
                //timeStamp += time.Minute < 10 ? "0" + time.Minute : "" + time.Minute;
                //timeStamp += ":";
                //timeStamp += time.Second < 10 ? "0" + time.Second : "" + time.Second;
                //timeStamp += ".";
                //if (time.Millisecond > 99)
                //{
                //    timeStamp += "" + time.Millisecond;
                //}
                //else if (time.Millisecond > 9 && time.Millisecond <= 99)
                //{
                //    timeStamp += "0" + time.Millisecond;

                //}
                //else
                //{
                //    timeStamp += "00" + time.Millisecond;

                //}


                attendanceJson += "\"title\":\"" + (attendance.ActionType == (int)AttendanceType.In ? "Clock in" : "Clock out") + "\",";
                attendanceJson += "\"start\":" + timeStamp + ",";
                attendanceJson += "\"end\":" + timeStamp + "";
                //attendanceJson += "\"start\":\"" + timeStamp + "\",";
                //attendanceJson += "\"end\":\"" + timeStamp + "\"";

                attendanceJson += "}";
            }
            attendanceJson += "]";

            return Content(attendanceJson);
        }

        public IActionResult Attendance(string d)
        {
            int? uid = HttpContext.Session.GetInt32("uid");
            if (uid == null)
            {
                return RedirectToAction("Login", "Login");
            }
            Uzer user = UserService.FindUserByID((int)uid);


            if (user.Department == null)
            {
                return Content("You do not have a department");
            }

            Department userDep = DepartmentService.FindDepartmentByID((int)user.Department);

            List<Department> deps = new List<Department>();
            Department selDep = null;
            List<Uzer> staffs = new List<Uzer>();


            //hard code, a little bit ugly
            if (userDep.Name.Equals("HR"))
            {
                //HR department can check other guys' attendance.
                deps = DepartmentService.FindDepartment();
                int depId = 0;
                Int32.TryParse(d, out depId);

                foreach (Department department in deps)
                {
                    if (department.Id == depId)
                    {
                        selDep = department;
                        break;
                    }
                }
                if (selDep == null && deps.Count > 0)
                {
                    selDep = deps[0];
                }


                if (selDep.Name.Equals("HR"))
                {
                    //user of HR department can only check the attendance of the staffs whose level is lower than him/her
                    staffs = UserService.FindUserByDepartmentAndUnderLevel((int)user.Department, (int)user.UzerLevel);
                    staffs.Insert(0, user);

                }
                else
                {
                    staffs = UserService.FindUserByDepartment(selDep.Id);

                }

            }
            else
            {
                //other guys can only check the attendance of the staffs with lower lever in the same department
                selDep = userDep;
                staffs = UserService.FindUserByDepartmentAndUnderLevel((int)user.Department, (int)user.UzerLevel);
                staffs.Insert(0, user);

            }


            AttendanceAttendanceViewModel attendanceAttendanceViewModel = new AttendanceAttendanceViewModel(deps, staffs, selDep);
            return View(attendanceAttendanceViewModel);
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
                    List<Attendance> clockOuts = AttendanceService.FindAttendanceByUserAndDateAndType((int)uid, t, AttendanceType.Out);
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

