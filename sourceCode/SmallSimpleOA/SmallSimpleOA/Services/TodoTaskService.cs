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

        public static TodoTask FindTodoTaskById(int id)
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.TodoTask.Single(t => t.Id.Equals(id) && t.Valid == true);

        }

        public static int FindCountByUser(int uid)
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.TodoTask.Count(t => t.UzerId.Equals(uid) && t.Valid == true);
        }

        public static int FindUndoneCountByUser(int uid)
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            return ctx.TodoTask.Count(t => t.UzerId.Equals(uid) && t.Status != (int)TodoTaskStatus.Done && t.Valid == true);
        }

        public static List<TodoTask> FindUndoneTodoTaskByUserAndPageAndPagesize(int uid, int page, int size)
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            List<TodoTask> res = ctx.TodoTask.Where(t => t.UzerId.Equals(uid) && t.Status != (int)TodoTaskStatus.Done && t.Valid == true).Skip((page - 1) * size).Take(size).OrderBy(t => t.DeadLine).ToList();
            UpdateTodoStatus(res);
            return res;
        }

        public static List<TodoTask> FindTaskByUserAndPageAndPagesize(int uid, int page, int size)
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            List<TodoTask> res = ctx.TodoTask.Where(t => t.UzerId.Equals(uid) && t.Valid == true).Skip((page - 1) * size).Take(size).OrderBy(t => t.DeadLine).ToList();
            UpdateTodoStatus(res);
            return res;
        }

        public static TodoTask AddTodoTask(int uid, string title, string content, DateTime deadline, TodoTaskStatus status)
        {
            TodoTask todo = new TodoTask();
            todo.Valid = true;
            todo.CreateTime = DateTime.Now;
            todo.UzerId = uid;
            todo.Title = title;
            todo.Content = content;
            todo.DeadLine = deadline;
            todo.Status = (int)status;

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            ctx.Add(todo);
            ctx.SaveChanges();
            return todo;
        }

        public static void UpdateTodoTask(TodoTask t)
        {
            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            ctx.Update(t);
            ctx.SaveChanges();
        }

        private static void UpdateTodoStatus(List<TodoTask> todos)
        {

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            DateTime now = DateTime.Now;
            foreach (TodoTask todo in todos)
            {
                if (todo.Status == (int)TodoTaskStatus.Done)
                {
                    continue;
                }
                double spanDays = ((DateTime)todo.DeadLine - now).TotalDays;

                TodoTaskStatus status;

                if (spanDays > 90)
                {
                    status = TodoTaskStatus.Faraway;
                }
                else if (spanDays > 30)
                {
                    status = TodoTaskStatus.Approching;
                }
                else if (spanDays > 0)
                {
                    status = TodoTaskStatus.DueSoon;
                }
                else
                {
                    status = TodoTaskStatus.Due;

                }
                todo.Status = (int)status;
                ctx.Update(todo);

            }

            ctx.SaveChanges();
        }

        private static void UpdateTodoStatus(TodoTask todo)
        {

            if (todo.Status == (int)TodoTaskStatus.Done)
            {
                return;
            }

            SmallSimpleOAContext ctx = new SmallSimpleOAContext();
            DateTime now = DateTime.Now;

            double spanDays = ((DateTime)todo.DeadLine - now).TotalDays;

            TodoTaskStatus status;

            if (spanDays > 90)
            {
                status = TodoTaskStatus.Faraway;
            }
            else if (spanDays > 30)
            {
                status = TodoTaskStatus.Approching;
            }
            else
            {
                status = TodoTaskStatus.DueSoon;
            }
            todo.Status = (int)status;
            ctx.Update(todo);
            ctx.SaveChanges();
        }
    }
}
