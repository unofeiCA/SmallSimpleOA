using System;
using SmallSimpleOA.Models;
using System.Collections.Generic;
using SmallSimpleOA.Utilities;

namespace SmallSimpleOA.ViewModels
{
    public class TodoListViewModel
    {

        public int Pages { get; private set; }
        public int CurrentPage { get; private set; }
        public List<TodoTask> TodoList { get; private set; }
        public int[] PageItems { get; private set; }
        private int MaxPageItemCount = 5;


        public TodoListViewModel(List<TodoTask> todos, int ps, int p)
        {

            TodoList = todos;
            Pages = ps;
            CurrentPage = p;
            PageItems = PageItemUtil.GeneratePageItems(ps, p, MaxPageItemCount);
        }
    }
}
