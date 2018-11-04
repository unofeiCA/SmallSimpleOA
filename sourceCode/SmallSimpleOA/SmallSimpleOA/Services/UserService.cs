using System;
using SmallSimpleOA.Models;
using System.Linq;

namespace SmallSimpleOA.Services
{
    public class UserService
    {
        public UserService()
        {
        }

        public static Uzer FindUserByEmail(string email)
        {
            return new SmallSimpleOAContext().Uzer.Single(e => e.Email.Equals(email));

        }
        public static void UpdateUser(Uzer u)
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            ctx.Update(u);
            ctx.SaveChanges();
        }
    }
}
