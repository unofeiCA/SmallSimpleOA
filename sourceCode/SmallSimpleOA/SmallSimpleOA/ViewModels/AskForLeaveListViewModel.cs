using System;
using System.Collections.Generic;
using SmallSimpleOA.Models;

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

            if (Pages > MaxPageItemCount)
            {
                PageItems = new int[MaxPageItemCount];
                int halfBeforeCurrent = PageItems.Length % 2 == 0 ? PageItems.Length / 2 - 1 : PageItems.Length / 2;
                PageItems[halfBeforeCurrent] = CurrentPage;

                int item = CurrentPage;
                int before = 0;
                for (int i = 0; i < halfBeforeCurrent; i++)
                {
                    if (item <= 1)
                    {
                        break;
                    }
                    item -= 1;
                    PageItems[halfBeforeCurrent - (i + 1)] = item;
                    before++;
                }

                int halfAfterCurrent = PageItems.Length - halfBeforeCurrent - 1;
                item = CurrentPage;
                for (int i = 0; i < halfAfterCurrent; i++)
                {
                    item += 1;
                    PageItems[halfBeforeCurrent + (i + 1)] = item;
                }
            }
            else
            {
                PageItems = new int[Pages];
                for (int i = 0; i < Pages; i++)
                {
                    PageItems[i] = i + 1;
                }
            }
        }
    }
}
