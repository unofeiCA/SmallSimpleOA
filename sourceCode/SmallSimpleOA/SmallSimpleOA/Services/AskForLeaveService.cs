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

        public static AskForLeave FindAskForLeaveById(int id)
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.AskForLeave.Single(a => a.Id.Equals(id) && a.Valid == true);
        }

        public static int FindCountByCurrentAt(int uid)
        {

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.AskForLeave.Count(a => a.CurrentAt.Equals(uid) && a.Valid == true);

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
            return ctx.AskForLeave.Where(a => a.CurrentAt.Id.Equals(uid) && a.Valid == true).Skip((page - 1) * size).Take(size).OrderByDescending(a => a.AppTime).ToList();

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

        public static void AddAskForLeave(Uzer applicant, DateTime startTime, DateTime endTime, DateTime appTime, string reason, string memo)
        {
            AskForLeave afl = new AskForLeave();
            afl.Valid = true;
            afl.Applicant = applicant;
            applicant.AskForLeaves.Add(afl);
            afl.StartTime = startTime;
            afl.EndTime = endTime;
            afl.AppTime = appTime;
            afl.Reason = reason;
            afl.Memo = memo;
            afl.Status = (int)AskForLeaveStatus.Applied;

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            ctx.Add(afl);
            ctx.SaveChanges();


            Uzer u = UserService.FindSupervisorByUid(applicant.Id);
            if (u == null)
            {
                afl.Status = (int)AskForLeaveStatus.Approved;
                afl.CurrentAt = applicant;
                applicant.AskForLeaves.Add(afl);
            }
            else
            {
                afl.CurrentAt = u;
                u.LeaveRequests.Add(afl);
            }
            ctx.Update(afl);
            ctx.SaveChanges();

        }

    }
}
