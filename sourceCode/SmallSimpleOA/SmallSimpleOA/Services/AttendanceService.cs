using System;
using System.Collections.Generic;
using SmallSimpleOA.Models;
using System.Linq;

namespace SmallSimpleOA.Services
{
    public class AttendanceService
    {

        public AttendanceService()
        {
        }

        public static List<Attendance> FindAttendanceByUserAndDate(int uid, DateTime dateTime)
        {
            int year = dateTime.Year;
            int month = dateTime.Month;
            int day = dateTime.Day;
            DateTime timeStart = new DateTime(year, month, day, 0, 0, 0);

            dateTime.AddDays(1);
            year = dateTime.Year;
            month = dateTime.Month;
            day = dateTime.Day;
            DateTime timeEnd = new DateTime(year, month, day, 0, 0, 0);

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.Attendance.Where(a => a.UzerId.Equals(uid) && a.ActionTime >= timeStart && a.ActionTime < timeEnd && a.Valid == true).ToList();
        
        }

        public static List<Attendance> FindAttendanceByUserWithinDates(int uid, DateTime startDate, DateTime endDate)
        {
            int year = startDate.Year;
            int month = startDate.Month;
            int day = startDate.Day;
            DateTime timeStart = new DateTime(year, month, day, 0, 0, 0);
           
            year = endDate.Year;
            month = endDate.Month;
            day = endDate.Day;
            DateTime timeEnd = new DateTime(year, month, day, 0, 0, 0);

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.Attendance.Where(a => a.UzerId.Equals(uid) && a.ActionTime >= timeStart && a.ActionTime < timeEnd && a.Valid == true).ToList();

        }

        public static void UpdateAttendance(Attendance a)
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            ctx.Update(a);
            ctx.SaveChanges();
        }
    }
}
