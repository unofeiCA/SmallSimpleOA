using System;
namespace SmallSimpleOA.ViewModels
{
    public class TodoNewViewModel
    {

        public DateTime DeadLine { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int LastNewSuccess = 0;
        public string LastNewPrompt;


        public TodoNewViewModel()
        {
        }

    }
}
