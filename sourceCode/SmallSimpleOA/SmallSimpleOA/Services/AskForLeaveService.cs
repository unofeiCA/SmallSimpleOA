using System;
using System.Collections.Generic;
using SmallSimpleOA.Models;
using System.Linq;

namespace SmallSimpleOA.Services
{
    public class AskForLeaveService
    {
        public AskForLeaveService()
        {
        }

        public static List<AskForLeave> FindAskForLeaveByApplicant(int uid)
        {

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.AskForLeave.Where(a => a.Applicant.Equals(uid) && a.Valid == true).ToList();

        }

        public static List<AskForLeave> FindAskForLeaveByCurrentAt(int uid)
        {

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.AskForLeave.Where(a => a.CurrentAt.Equals(uid) && a.Valid == true).ToList();

        }

        public static void UpdateAskForLeave(AskForLeave a)
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            ctx.Update(a);
            ctx.SaveChanges();
        }
    }
}
