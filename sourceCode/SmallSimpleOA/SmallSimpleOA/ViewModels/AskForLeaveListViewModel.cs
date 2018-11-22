using System;
using System.Collections.Generic;
using SmallSimpleOA.Models;
using SmallSimpleOA.Utilities;

namespace SmallSimpleOA.ViewModels
{
    public class AskForLeaveListViewModel
    {
        public int Pages { get; private set; }
        public int CurrentPage { get; private set; }
        public List<AskForLeave> AskForLeavesList { get; private set; }
        public int[] PageItems { get; private set; }
        private int MaxPageItemCount = 5;

        public AskForLeaveListViewModel(List<AskForLeave> asks, int ps, int p)
        {
            AskForLeavesList = asks;
            Pages = ps;
            CurrentPage = p;
            PageItems = PageItemUtil.GeneratePageItems(ps, p, MaxPageItemCount);

        }
    }
}
