using System;
using SmallSimpleOA.Models;
using System.Linq;
using SmallSimpleOA.Utilities;
using System.Collections.Generic;

namespace SmallSimpleOA.Services
{
    public class UserService
    {
        public UserService()
        {
        }

        public static List<Uzer> FindUserByDepartment(int depId)
        {
            return new SmallSimpleOAContext().Uzer.Where(e => e.Department == depId && e.Valid == true).ToList();

        }

        public static List<Uzer> FindUserByDepartmentAndUnderLevel(int depId, int level)
        {
            return new SmallSimpleOAContext().Uzer.Where(e => e.Department == depId && e.UzerLevel < level && e.Valid == true).ToList();

        }

        public static Uzer FindUserByID(int uid)
        {
            return new SmallSimpleOAContext().Uzer.Single(e => e.Id == uid && e.Valid == true);

        }

        public static Uzer FindUserByEmail(string email)
        {
            return new SmallSimpleOAContext().Uzer.Single(e => e.Email.Equals(email) && e.Valid == true);

        }
        public static void UpdateUser(Uzer u)
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            ctx.Update(u);
            ctx.SaveChanges();
        }

        public static void AddUser(string email, string password, string firstName, string lastName, int department, int level)
        {
            Uzer u = new Uzer();
            u.Valid = true;
            u.Email = email;
            u.FirstName = firstName;
            u.LastName = lastName;
            u.Department = department;
            u.UzerLevel = level;
            string salt = UserPasswordUtil.GenerateSalt();
            u.Salt = salt;
            u.Password = UserPasswordUtil.GeneratePasswordAfterSalt(password, salt);
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            ctx.Add(u);
            ctx.SaveChanges();
        }

        public static Uzer FindSupervisorByUid(int uid)
        {
            Uzer u = FindUserByID(uid);
            Department department = DepartmentService.FindDepartmentByID((int)u.Department);
            if (u.UzerLevel >= department.TotalStaffLevel)
            {
                return null;
            }
            Uzer supervisor = new SmallSimpleOAContext().Uzer.Single(s => s.Valid == true && s.Department == u.Department && s.UzerLevel == (u.UzerLevel + 1));
            return supervisor;
        }
    }
}
