using System;
using SmallSimpleOA.Models;

namespace SmallSimpleOA.ViewModels
{
    public class AskForLeaveDetailViewModel
    {
        public AskForLeave AskForLeave { get; private set; }

        public AskForLeaveDetailViewModel(AskForLeave ask)
        {
            AskForLeave = ask;
        }
    }
}
