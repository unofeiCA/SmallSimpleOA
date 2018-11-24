using System;
using System.Collections.Generic;
using SmallSimpleOA.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SmallSimpleOA.Services
{
    public class AskForLeaveService
    {
        public AskForLeaveService()
        {
        }

        public static AskForLeave FindAskForLeaveById(int id)
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.AskForLeave.Single(a => a.Id.Equals(id) && a.Valid == true);
        }

        public static int FindCountByCurrentAt(int uid)
        {

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.AskForLeave.Count(a => a.CurrentAtId.Equals(uid) && a.Valid == true);

        }

        public static int FindCountByApplicant(int uid)
        {

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.AskForLeave.Count(a => a.Applicant.Equals(uid) && a.Valid == true);

        }

        public static List<AskForLeave> FindAskForLeaveByApplicantAndPageAndPagesize(int uid, int page, int size)
        {

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.AskForLeave.Where(a => a.Applicant.Equals(uid) && a.Valid == true).Skip((page - 1) * size).Take(size).OrderByDescending(a => a.AppTime).ToList();

        }

        public static List<AskForLeave> FindAskForLeaveByCurrentAtAndPageAndPagesize(int uid, int page, int size)
        {

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.AskForLeave.Where(a => a.CurrentAtId.Equals(uid) && a.Valid == true).Skip((page - 1) * size).Take(size).OrderByDescending(a => a.AppTime).ToList();

        }


        public static List<AskForLeave> FindAskForLeaveByCurrentAt(int uid)
        {

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.AskForLeave.Where(a => a.CurrentAtId.Equals(uid) && a.Valid == true).ToList();

        }

        public static void UpdateAskForLeave(AskForLeave a)
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            ctx.Update(a);
            ctx.SaveChanges();
        }

        public static AskForLeave AddAskForLeave(int uid, DateTime startTime, DateTime endTime, DateTime appTime, string reason, string memo)
        {
            AskForLeave afl = new AskForLeave();
            afl.Valid = true;
            afl.StartTime = startTime;
            afl.EndTime = endTime;
            afl.AppTime = appTime;
            afl.Reason = reason;
            afl.Memo = memo;
            afl.Status = (int)AskForLeaveStatus.Applied;
            afl.ApplicantId = uid;

            //           applicant.AskForLeaves.Add(afl);
            //           ctx.Update(afl);



            //ctx.Update(afl);

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            ctx.Add(afl);
            ctx.SaveChanges();
            return afl;
        }

    }
}
