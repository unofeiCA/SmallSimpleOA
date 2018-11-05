using System;
using SmallSimpleOA.Models;
using System.Linq;
using SmallSimpleOA.Utilities;

namespace SmallSimpleOA.Services
{
    public class UserService
    {
        public UserService()
        {
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
    }
}
