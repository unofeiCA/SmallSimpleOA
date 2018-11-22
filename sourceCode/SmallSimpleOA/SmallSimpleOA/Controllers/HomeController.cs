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
        public IActionResult Home(string ep, string tp)
        {

            const int PAGE_SIZE = 10;
            int? uid = HttpContext.Session.GetInt32("uid");
            if (uid == null)
            {
                return RedirectToAction("Login", "Login");
            }
            int eventPage, todoPage;
            if (!Int32.TryParse(ep, out eventPage))
            {
                eventPage = 1;
            }
            if (!Int32.TryParse(tp, out todoPage))
            {
                todoPage = 1;
            }

            int eventCount = AskForLeaveService.FindCountByCurrentAt((int)uid);
            int todoCount = 0;//TodoTaskService.FindCountByUser((int)uid);

            int eventPages = eventCount % PAGE_SIZE == 0 ? eventCount / PAGE_SIZE : eventCount / PAGE_SIZE + 1;
            int todoPages = todoCount % PAGE_SIZE == 0 ? todoCount / PAGE_SIZE : todoCount / PAGE_SIZE + 1;

            if (eventPage > eventPages)
            {
                eventPage = eventPages;
            }
            if (todoPage > todoPages)
            {
                todoPage = todoPages;
            }

            if (eventPage <= 0)
            {
                eventPage = 1;
            }
            if (todoPage <= 0)
            {
                todoPage = 1;
            }

            List<AskForLeave> asks = AskForLeaveService.FindAskForLeaveByCurrentAtAndPageAndPagesize((int)uid, eventPage, PAGE_SIZE);
            List<TodoTask> todos = new List<TodoTask>();//TodoTaskService.FindTodoTaskByUserAndDateAndPageAndPagesize((int)uid, DateTime.Now, todoPage, PAGE_SIZE);

            List<Attendance> attendanceList = AttendanceService.FindAttendanceByUserAndDateAndType((int)uid, DateTime.Now, AttendanceType.In);
            Boolean need = (attendanceList == null || attendanceList.Count <= 0) ? true : false;
            HomeHomeViewModel homeHomeViewModel = new HomeHomeViewModel(asks, eventPages, eventPage, todos, todoPages, todoPage, need);
            return View(homeHomeViewModel);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
