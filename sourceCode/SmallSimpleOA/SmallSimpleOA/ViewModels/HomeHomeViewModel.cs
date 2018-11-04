using System;
using System.Collections.Generic;
using SmallSimpleOA.Models;

namespace SmallSimpleOA.ViewModels
{
    public class HomeHomeViewModel
    {
        public List<TodoTask> TodoTaskList { get; set; }
        public List<AskForLeave> AskForLeaveList { get; set; }
        public Boolean NeedClockIn { get; set; }

        public HomeHomeViewModel(List<AskForLeave> askForLeaveList, List<TodoTask> todoTaskList, Boolean needClockIn)
        {
            TodoTaskList = todoTaskList;
            AskForLeaveList = askForLeaveList;
            NeedClockIn = needClockIn;
        }
    }
}
