using System;
using System.Collections.Generic;
using SmallSimpleOA.Models;

namespace SmallSimpleOA.ViewModels
{
    public class MessageMessageViewModel
    {
        public List<Department> DepartmentList { get; private set; }
        public List<Uzer> StaffOfDepartment { get; private set; }
        public Department SelDep { get;  private set;}

        public MessageMessageViewModel(List<Department> departments, List<Uzer> staffOfDep, Department dep)
        {
            DepartmentList = departments;
            StaffOfDepartment = staffOfDep;
            SelDep = dep;
        }
    }
}
