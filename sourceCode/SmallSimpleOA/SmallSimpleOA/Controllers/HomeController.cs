using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SmallSimpleOA.Models;
using SmallSimpleOA.ViewModels;
using SmallSimpleOA.Services;
namespace SmallSimpleOA.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            int? uid = HttpContext.Session.GetInt32("uid");
            if(uid == null)
            {
                return RedirectToAction("Login", "Login");
            }
            List<AskForLeave> askForLeaveList = AskForLeaveService.FindAskForLeaveByCurrentAt((int)uid);
            List<TodoTask> todoTaskList = TodoTaskService.FindTodoTaskByUserAndDate((int)uid, DateTime.Now);
            List<Attendance> attendanceList = AttendanceService.FindAttendanceByUserAndDate((int)uid, DateTime.Now);
            Boolean need = (attendanceList == null || attendanceList.Count <= 0) ? true : false;
            HomeHomeViewModel homeHomeViewModel = new HomeHomeViewModel(askForLeaveList, todoTaskList, need);
            return View(homeHomeViewModel);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
