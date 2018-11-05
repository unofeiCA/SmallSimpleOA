using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallSimpleOA.Services;
using SmallSimpleOA.ViewModels;
using SmallSimpleOA.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmallSimpleOA.Controllers
{
    public class StaffManageController : Controller
    {
        public IActionResult New(string r)
        {
            StaffManageNewViewModel staffManageNewViewModel = new StaffManageNewViewModel(DepartmentService.FindDepartment());
            if(r == null)
            {
                staffManageNewViewModel.LastNewSuccess = 0;

            }
            else if(r.Equals("0"))
            {
                staffManageNewViewModel.LastNewSuccess = 1;
                staffManageNewViewModel.LastNewPrompt = "Add new user successful!";
            }
            else
            {
                staffManageNewViewModel.LastNewSuccess = -1;
                staffManageNewViewModel.LastNewPrompt = r;
            }
            return View(staffManageNewViewModel);
        }

        public IActionResult DoNew(string email, string password, string firstName, string lastName, string department, string uzerLevel)
        {
            string result = "0";
            if (email == null ||
               password == null ||
               firstName == null ||
               lastName == null ||
               department == null ||
               uzerLevel == null ||
               email.Trim().Equals("") ||
               password.Trim().Equals("") ||
               firstName.Trim().Equals("") ||
               lastName.Trim().Equals("") ||
               department.Trim().Equals("") ||
               uzerLevel.Trim().Equals("")
              )
            {
                result = "All fields should be filled.";
                return RedirectToAction("New", "StaffManage", new { r = result });
            }
            Int32 dep, lev;
            if (!Int32.TryParse(department, out dep) || !Int32.TryParse(uzerLevel, out lev))
            {
                result = "Sorry unknown error, try again please.";
                return RedirectToAction("New", "StaffManage", new { r = result });
            }

            UserService.AddUser(email, password, firstName, lastName, dep, lev);

            return RedirectToAction("New", "StaffManage", new { r = result });
        }
    }
}
