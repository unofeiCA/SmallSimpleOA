using System;
namespace SmallSimpleOA.ViewModels
{
    public class AskFroLeaveNewViewModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Reason { get; set; }
        public string Memo { get; set; }
        public int LastNewSuccess = 0;
        public string LastNewPrompt;

        public AskFroLeaveNewViewModel()
        {

        }
    }
}
