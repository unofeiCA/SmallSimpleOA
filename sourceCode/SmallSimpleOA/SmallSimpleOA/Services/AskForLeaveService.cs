﻿using System;
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

        public static void AddAskForLeave(int applicant, DateTime startTime, DateTime endTime, DateTime appTime, string reason, string memo)
        {
            AskForLeave afl = new AskForLeave();
            afl.Valid = true;
            afl.Applicant = applicant;
            afl.StartTime = startTime;
            afl.EndTime = endTime;
            afl.AppTime = appTime;
            afl.Reason = reason;
            afl.Memo = memo;
            afl.Status = (int)AskForLeaveStatus.Applied;

            Uzer u = UserService.FindSupervisorByUid(applicant);
            if (u == null)
            {
                afl.Status = (int)AskForLeaveStatus.Approved;
                afl.CurrentAt = applicant;

            }
            else
            {
                afl.CurrentAt = u.Id;
            }

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            ctx.Add(afl);
            ctx.SaveChanges();
        }

    }
}