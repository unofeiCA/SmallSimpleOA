using System;
using System.Collections.Generic;
using SmallSimpleOA.Models;
using System.Linq;

namespace SmallSimpleOA.Services
{
    public class TodoTaskService
    {
        public TodoTaskService()
        {
        }

        public static List<TodoTask> FindTodoTaskByUserAndDate(int uid, DateTime dateTime)
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
            return ctx.TodoTask.Where(t => t.UzerId.Equals(uid) && t.DeadLine >= timeStart && t.DeadLine < timeEnd && t.Valid == true).ToList();

        }


        public static void UpdateTodoTask(TodoTask t)
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            ctx.Update(t);
            ctx.SaveChanges();
        }
    }
}
