using System;
using System.Collections.Generic;
using SmallSimpleOA.Models;

namespace SmallSimpleOA.ViewModels
{
    public class StaffManageNewViewModel
    {
        public string FirstName;
        public string LastName;
        public int Department;
        public int UzerLevel;
        public string Email;
        public string Password;
        public int LastNewSuccess = 0;
        public string LastNewPrompt;

        public List<Department> DepartmentList { get; private set; }

        public StaffManageNewViewModel(List<Department> departments)
        {
            DepartmentList = departments;
        }
    }
}
