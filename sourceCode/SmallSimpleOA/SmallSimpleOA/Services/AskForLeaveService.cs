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
            AskForLeave ask = ctx.AskForLeave.FirstOrDefault(a => a.Id.Equals(id) && a.Valid == true);
            ctx.Entry(ask).Reference(a => a.CurrentAt).Load();
            ctx.Entry(ask).Reference(a => a.Applicant).Load();
            return ask;
        }

        public static int FindCountByCurrentAt(Uzer u)
        {

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.AskForLeave.Count(a => a.CurrentAt.Equals(u) && a.Valid == true);
        }

        public static int FindCountByApplicant(Uzer u)
        {

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.AskForLeave.Count(a => a.Applicant.Equals(u) && a.Valid == true);

        }

        public static List<AskForLeave> FindAskForLeaveByApplicantAndPageAndPagesize(Uzer u, int page, int size)
        {

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.AskForLeave.Where(a => a.Applicant.Equals(u) && a.Valid == true).OrderByDescending(a => a.AppTime).Skip((page - 1) * size).Take(size).ToList();

        }

        public static List<AskForLeave> FindAskForLeaveByCurrentAtAndPageAndPagesize(Uzer u, int page, int size)
        {

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.AskForLeave.Where(a => a.CurrentAt.Equals(u) && a.Valid == true).OrderByDescending(a => a.AppTime).Skip((page - 1) * size).Take(size).ToList();

        }


        public static List<AskForLeave> FindAskForLeaveByCurrentAt(Uzer u)
        {

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.AskForLeave.Where(a => a.CurrentAt.Equals(u) && a.Valid == true).ToList();

        }

        public static void UpdateAskForLeave(AskForLeave a)
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            ctx.Update(a);
            ctx.SaveChanges();
        }

        public static AskForLeave AddAskForLeave(Uzer u, DateTime startTime, DateTime endTime, DateTime appTime, string reason, string memo)
        {
            AskForLeave afl = new AskForLeave();
            afl.Valid = true;
            afl.StartTime = startTime;
            afl.EndTime = endTime;
            afl.AppTime = appTime;
            afl.Reason = reason;
            afl.Memo = memo;
            afl.Status = (int)AskForLeaveStatus.Applied;

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            ctx.Update(u);
            u.AskForLeaves.Add(afl);
            u.LeaveRequests.Add(afl);
            ctx.SaveChanges();
            return afl;
        }

        public static void AskFlowToNextSupervisor(AskForLeave ask) 
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();

            Uzer supervisor = UserService.FindSupervisorByUid(ask.CurrentAt.Id);
            if (supervisor == null)
            {
                ctx.Update(ask);
                ask.Status = (int)AskForLeaveStatus.Approved;
                ask.CurrentAt.LeaveRequests.Remove(ask);
                ctx.SaveChanges();

            }
            else
            {

                ctx.Update(ask);
                ask.Status = (int)AskForLeaveStatus.Reviewing;
                ask.CurrentAt.LeaveRequests.Remove(ask);
                ctx.SaveChanges();
                ctx.Update(supervisor);
                supervisor.LeaveRequests.Add(ask);
                ctx.SaveChanges();

            }

        }

        public static void RejectAskForLeave(AskForLeave ask) 
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            ctx.Update(ask);
            ask.CurrentAt.LeaveRequests.Remove(ask);
            ask.Status = (int)AskForLeaveStatus.Rejected;
            ctx.SaveChanges();
        }
    }
}
