using System;
using System.Collections.Generic;
using SmallSimpleOA.Models;
using System.Linq;

namespace SmallSimpleOA.Services
{
    public class DepartmentService
    {
        public DepartmentService()
        {
        }

        public static List<Department> FindDepartment()
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.Department.Where(d => d.Valid == true).ToList();

        }


        public static void UpdateDepartment(Department d)
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            ctx.Update(d);
            ctx.SaveChanges();
        }
    }
}
