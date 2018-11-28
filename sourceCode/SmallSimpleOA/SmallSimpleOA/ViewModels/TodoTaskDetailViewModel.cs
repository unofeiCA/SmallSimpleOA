using System;
using SmallSimpleOA.Models;
namespace SmallSimpleOA.ViewModels
{
    public class TodoTaskDetailViewModel
    {
        public TodoTask Todo { get; private set; }
        public int Id { get; set; }
        public DateTime DeadLine { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int LastUpdateSuccess = 0;
        public string LastUpdatePrompt;

        public string F { get; private set; }

        public void SetFrom(string f)
        {
            F = f;
        }

        public TodoTaskDetailViewModel(TodoTask todo)
        {
            Todo = todo;
        }
    }
}
