using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallSimpleOA.ViewModels;
using SmallSimpleOA.Models;
using SmallSimpleOA.Services;


namespace SmallSimpleOA.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Message(string d)
        {
            List<Department> deps = DepartmentService.FindDepartment();
            List<Uzer> staffs = new List<Uzer>();
            int depId = 0;
            Int32.TryParse(d, out depId);
               
            Department selDep = null;
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
            staffs = UserService.FindUserByDepartment(selDep.Id);


            MessageMessageViewModel messageMessageViewModel = new MessageMessageViewModel(deps, staffs, selDep); 
            return View(messageMessageViewModel);
        }


    }
}
