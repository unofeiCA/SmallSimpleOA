using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallSimpleOA.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using SmallSimpleOA.Models;
using SmallSimpleOA.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmallSimpleOA.Controllers
{
    public class TodoController : Controller
    {
        public IActionResult List(string p)
        {
            const int PAGE_SIZE = 10;
            int? uid = HttpContext.Session.GetInt32("uid");
            if (uid == null)
            {
                return RedirectToAction("Login", "Login");
            }
            int page;
            if (!Int32.TryParse(p, out page))
            {
                page = 1;
            }

            int count = TodoTaskService.FindCountByUser((int)uid);
            int pages = count % PAGE_SIZE == 0 ? count / PAGE_SIZE : count / PAGE_SIZE + 1;

            if (page > pages)
            {
                page = pages;
            }

            if (page <= 0)
            {
                page = 1;
            }

            List<TodoTask> todos = TodoTaskService.FindTaskByUserAndPageAndPagesize((int)uid, page, PAGE_SIZE);

            TodoListViewModel todoListViewModel = new TodoListViewModel(todos, pages, page);

            return View(todoListViewModel);
        }

        public IActionResult New(string r)
        {
            TodoNewViewModel todoNewViewModel = new TodoNewViewModel();

            if (r == null)
            {
                todoNewViewModel.LastNewSuccess = 0;

            }
            else if (r.Equals("0"))
            {
                todoNewViewModel.LastNewSuccess = 1;
               // todoNewViewModel.LastNewPrompt = "You have applied a request of leave, please wait for your supervisors to review.";
            }
            else
            {
                todoNewViewModel.LastNewSuccess = -1;
                todoNewViewModel.LastNewPrompt = r;
            }
            return View(todoNewViewModel);

        }


        [HttpPost]
        public IActionResult DoNew(string deadline, string title, string content)
        {

            string result = "0";
            if (deadline == null ||
               title == null ||
               deadline.Trim().Equals("") ||
               title.Trim().Equals("") 
              )
            {
                result = "Fields of 'Title' and 'DeadLine' should be filled.";
                return RedirectToAction("New", "Todo", new { r = result });
            }

            int? uid = HttpContext.Session.GetInt32("uid");

            DateTime now = DateTime.Now;
            DateTime dl;
            if (!DateTime.TryParse(deadline, out dl))
            {
                result = "Time format not valid.";
                return RedirectToAction("New", "Todo", new { r = result });
            }
            if (dl < now)
            {
                result = "DeadLine is earlier than now.";
                return RedirectToAction("New", "Todo", new { r = result });
            }

            if (uid == null)
            {
                return RedirectToAction("Login", "Login");

            }

            double spanDays = (dl - now).TotalDays;

            TodoTaskStatus status;

            if (spanDays > 90)
            {
                status = TodoTaskStatus.Faraway;
            }
            else if (spanDays > 30)
            {
                status = TodoTaskStatus.Approching;
            }
            else
            {
                status = TodoTaskStatus.DueSoon;
            }

            TodoTaskService.AddTodoTask((int)uid, title, content, dl, status);

            return RedirectToAction("Home", "Home");
        }

        public IActionResult Detail(string id, string r)
        {
            int? uid = HttpContext.Session.GetInt32("uid");
            if (uid == null)
            {
                return RedirectToAction("Login", "Login");
            }
            int i;
            if (null == id || !Int32.TryParse(id, out i))
            {
                return RedirectToAction("Home", "Home");
            }

            TodoTask todo = TodoTaskService.FindTodoTaskById(i);
            TodoTaskDetailViewModel model = new TodoTaskDetailViewModel(todo);

            if (r == null)
            {
                model.LastUpdateSuccess = 0;

            }
            else if (r.Equals("0"))
            {
                model.LastUpdateSuccess = 1;
                // todoNewViewModel.LastNewPrompt = "You have applied a request of leave, please wait for your supervisors to review.";
            }
            else
            {
                model.LastUpdateSuccess = -1;
                model.LastUpdatePrompt = r;
            }

            return View(model);
        }


        public IActionResult DoDone(string id)
        {
            int? uid = HttpContext.Session.GetInt32("uid");
            if (uid == null)
            {
                return RedirectToAction("Login", "Login");
            }
            int todoId;
            if (!Int32.TryParse(id, out todoId))
            {
                return RedirectToAction("Home", "Home");

            }
            TodoTask todo = TodoTaskService.FindTodoTaskById(todoId);

            if (todo == null)
            {
                return RedirectToAction("Home", "Home");

            }

            todo.Status = (int)TodoTaskStatus.Done;
            TodoTaskService.UpdateTodoTask(todo);

            return RedirectToAction("Home", "Home");
        }

        public IActionResult DoDelete(string id)
        {
            int? uid = HttpContext.Session.GetInt32("uid");
            if (uid == null)
            {
                return RedirectToAction("Login", "Login");
            }
            int todoId;
            if (!Int32.TryParse(id, out todoId))
            {
                return RedirectToAction("Home", "Home");

            }
            TodoTask todo = TodoTaskService.FindTodoTaskById(todoId);

            if (todo == null)
            {
                return RedirectToAction("Home", "Home");

            }

            todo.Valid = false;
            todo.ModifyTime = DateTime.Now;
            TodoTaskService.UpdateTodoTask(todo);

            return RedirectToAction("Home", "Home");
        }


        public IActionResult DoUpdate(string id, string title, string content, string deadline)
        {

            int? uid = HttpContext.Session.GetInt32("uid");
            if (uid == null)
            {
                return RedirectToAction("Login", "Login");
            }
            int todoId;
            if (!Int32.TryParse(id, out todoId))
            {
                return RedirectToAction("Home", "Home");

            }
            TodoTask todo = TodoTaskService.FindTodoTaskById(todoId);

            if (todo == null)
            {
                return RedirectToAction("Home", "Home");

            }

            string result = "0";
            if (deadline == null ||
               title == null ||
               deadline.Trim().Equals("") ||
               title.Trim().Equals("")
              )
            {
                result = "Fields of 'Title' and 'DeadLine' should be filled.";
                return RedirectToAction("Detail", "Todo", new { id = todoId, r = result });
            }

            DateTime now = DateTime.Now;
            DateTime dl;
            if (!DateTime.TryParse(deadline, out dl))
            {
                result = "Time format not valid.";
                return RedirectToAction("Detail", "Todo", new { id = todoId, r = result });
            }
            if (dl < now)
            {
                result = "DeadLine is earlier than now.";
                return RedirectToAction("Detail", "Todo", new { id = todoId, r = result });
            }


            double spanDays = (dl - now).TotalDays;

            TodoTaskStatus status;

            if (spanDays > 90)
            {
                status = TodoTaskStatus.Faraway;
            }
            else if (spanDays > 30)
            {
                status = TodoTaskStatus.Approching;
            }
            else
            {
                status = TodoTaskStatus.DueSoon;
            }

            todo.Content = content;
            todo.Title = title;
            todo.DeadLine = dl;
            todo.Status = (int)status;
            todo.ModifyTime = DateTime.Now;
            TodoTaskService.UpdateTodoTask(todo);

            return RedirectToAction("Home", "Home");

        }

    }
}
