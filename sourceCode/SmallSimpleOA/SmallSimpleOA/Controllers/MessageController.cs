using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SmallSimpleOA.ViewModels;
using SmallSimpleOA.Models;
using SmallSimpleOA.Services;
using SmallSimpleOA.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;

namespace SmallSimpleOA.Controllers
{
    public class MessageController : Controller
    {
        private const int MSG_PAGE_SIZE = 20;
        public IActionResult FetchMsg(string tId, string offset) 
        {
            
            int? uid = HttpContext.Session.GetInt32("uid");
            if (uid == null)
            {
                return RedirectToAction("Login", "Login");
            }
            int tid;
            if (!Int32.TryParse(tId, out tid))
            {
                return Content("");
            }

            List<Message> msgs;
            int ost;
            if (offset == null || !Int32.TryParse(offset, out ost))
            {
                msgs = MessageService.FindMessageByUserAndTargetAndAmount((int)uid, tid, MSG_PAGE_SIZE);
            }
            else
            {
                msgs = MessageService.FindMessageByUserAndTargetAndOffsetAndAmount((int)uid, tid, ost, MSG_PAGE_SIZE);
            }

            string msgJson = JsonConvert.SerializeObject(msgs);

            return Content(msgJson);
        }

        public IActionResult Message(string d, string u)
        {
            int? uid = HttpContext.Session.GetInt32("uid");
            if (uid == null)
            {
                return RedirectToAction("Login", "Login");
            }
            List<Department> deps = DepartmentService.FindDepartment();
            Department selDep = null;

            int targetId = 0;
            if (Int32.TryParse(u, out targetId) && targetId > 0) 
            {
                //find target's deparment
                Uzer target = UserService.FindUserByID(targetId);

                foreach (Department department in deps)
                {
                    if (department.Id == target.Department)
                    {
                        selDep = department;
                        break;
                    }
                }
            }
            else
            {
                //try find selected department
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
            }

            List<Uzer> staffs = new List<Uzer>();

            if (selDep == null && deps.Count > 0)
            {
                selDep = deps[0];
            }
            staffs = UserService.FindUserByDepartment(selDep.Id);


            MessageMessageViewModel messageMessageViewModel = new MessageMessageViewModel(deps, staffs, selDep, targetId); 
            return View(messageMessageViewModel);
        }


    }
}
