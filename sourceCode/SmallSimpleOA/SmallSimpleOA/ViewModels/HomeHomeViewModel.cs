using System;
using System.Collections.Generic;
using SmallSimpleOA.Models;
using SmallSimpleOA.Utilities;

namespace SmallSimpleOA.ViewModels
{
    public class HomeHomeViewModel
    {
        public List<TodoTask> TodoTaskList { get; set; }
        public List<AskForLeave> EventList { get; set; } // for now the events are always the leave request flowing to the user
        public Boolean NeedClockIn { get; set; }
        public int[] EventPageitems { get; private set; }
        public int[] TodoPageitems { get; private set; }
        public int EventPages { get; private set; }
        public int EventCurrentPage { get; private set; }
        public int TodoPages { get; private set; }
        public int TodoCurrentPage { get; private set; }

        private int MaxPageItemCount = 5;


        public HomeHomeViewModel(List<AskForLeave> eventList, int eventPages, int eventCurrentPage, List<TodoTask> todoTaskList, int todoPages, int todoCurrentPage, Boolean needClockIn)
        {
            EventPages = eventPages;
            EventCurrentPage = eventCurrentPage;
            EventList = eventList;

            TodoPages = todoPages;
            TodoCurrentPage = todoCurrentPage;
            TodoTaskList = todoTaskList;

            EventPageitems = PageItemUtil.GeneratePageItems(eventPages, eventCurrentPage, MaxPageItemCount);
            TodoPageitems = PageItemUtil.GeneratePageItems(todoPages, todoCurrentPage, MaxPageItemCount);
            NeedClockIn = needClockIn;

        }
    }
}
