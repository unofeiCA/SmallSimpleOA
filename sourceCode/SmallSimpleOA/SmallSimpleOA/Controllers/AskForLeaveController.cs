using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallSimpleOA.ViewModels;
using SmallSimpleOA.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using SmallSimpleOA.Models;
using SmallSimpleOA.Services;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmallSimpleOA.Controllers
{
    public class AskForLeaveController : Controller
    {
        // GET: /<controller>/
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

            int count = AskForLeaveService.FindCountByApplicant((int)uid);
            int pages = count % PAGE_SIZE == 0 ? count / PAGE_SIZE : count / PAGE_SIZE + 1;

            if (page > pages)
            {
                page = pages;
            }

            if (page <= 0)
            {
                page = 1;
            }

            List<AskForLeave> asks = AskForLeaveService.FindAskForLeaveByApplicantAndPageAndPagesize((int)uid, page, PAGE_SIZE);

            AskForLeaveListViewModel askForLeaveListViewModel = new AskForLeaveListViewModel(asks, pages, page);
            return View(askForLeaveListViewModel);
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


        [HttpPost]
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


            AskForLeave ask = AskForLeaveService.AddAskForLeave((int)uid, st, et, DateTime.Now, reason, memo);

            Uzer supervisor = UserService.FindSupervisorByUid((int)uid);
            if (supervisor == null)
            {
                ask.Status = (int)AskForLeaveStatus.Approved;
                ask.CurrentAtId = (int)uid;
            }
            else
            {
                ask.CurrentAtId = supervisor.Id;
            }
            AskForLeaveService.UpdateAskForLeave(ask);

            return RedirectToAction("New", "AskForLeave", new { r = result });
        }

        public IActionResult Detail(string id)
        {
            int? uid = HttpContext.Session.GetInt32("uid");
            if (uid == null)
            {
                return RedirectToAction("Login", "Login");
            }
            int i;
            if (null == id || !Int32.TryParse(id, out i))
            {
                return RedirectToAction("List", "AskForLeave");
            }

            AskForLeave ask = AskForLeaveService.FindAskForLeaveById(i);
            AskForLeaveDetailViewModel model = new AskForLeaveDetailViewModel(ask);
            return View(model);
        }

        public IActionResult DoAction(string id, string act)
        {
            int? uid = HttpContext.Session.GetInt32("uid");
            if (uid == null)
            {
                return RedirectToAction("Login", "Login");
            }
            int askId;
            if (!Int32.TryParse(id, out askId))
            {
                return RedirectToAction("List", "AskForLeave");

            }
            AskForLeave ask = AskForLeaveService.FindAskForLeaveById(askId);

            if (ask == null)
            {
                return RedirectToAction("List", "AskForLeave");

            }

            if (act.Equals("reject"))
            {
                ask.CurrentAtId = 0;
                ask.Status = (int)AskForLeaveStatus.Rejected;
                AskForLeaveService.UpdateAskForLeave(ask);
            }
            else if (act.Equals("approve"))
            {
                Uzer supervisor = UserService.FindSupervisorByUid((int)uid);
                if (supervisor == null)
                {
                    ask.CurrentAtId = 0;
                    ask.Status = (int)AskForLeaveStatus.Approved;
                    AskForLeaveService.UpdateAskForLeave(ask);
                }
                else
                {
                    ask.CurrentAtId = supervisor.Id;
                    ask.Status = (int)AskForLeaveStatus.Reviewing;
                    AskForLeaveService.UpdateAskForLeave(ask);
                }
            }
            return RedirectToAction("Home", "Home");
        }

    }
}
