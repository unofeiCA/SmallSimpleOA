using System;
namespace SmallSimpleOA.Utilities
{
    public class PageItemUtil
    {
        public PageItemUtil()
        {
        }

        public static int[] GeneratePageItems( int pages, int currentPage, int pageItemsCount )
        {
            int[] pageItems;
            if (pages > pageItemsCount)
            {
                pageItems = new int[pageItemsCount];
                int halfBeforeCurrent = pageItems.Length % 2 == 0 ? pageItems.Length / 2 - 1 : pageItems.Length / 2;
                pageItems[halfBeforeCurrent] = currentPage;

                int item = currentPage;
                int before = 0;
                for (int i = 0; i < halfBeforeCurrent; i++)
                {
                    if (item <= 1)
                    {
                        break;
                    }
                    item -= 1;
                    pageItems[halfBeforeCurrent - (i + 1)] = item;
                    before++;
                }

                int halfAfterCurrent = pageItems.Length - halfBeforeCurrent - 1;
                item = currentPage;
                for (int i = 0; i < halfAfterCurrent; i++)
                {
                    item += 1;
                    pageItems[halfBeforeCurrent + (i + 1)] = item;
                }
            }
            else
            {
                pageItems = new int[pages];
                for (int i = 0; i < pages; i++)
                {
                    pageItems[i] = i + 1;
                }
            }
            return pageItems;
        }
    }
}
