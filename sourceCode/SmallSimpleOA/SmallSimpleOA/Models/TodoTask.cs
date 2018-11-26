using System;
using System.Collections.Generic;

namespace SmallSimpleOA.Models
{
    public partial class TodoTask
    {
        public int Id { get; set; }
        public bool? Valid { get; set; } = true;
        public int? Status { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? ModifyTime { get; set; }
        public DateTime? DeadLine { get; set; }
        public int? UzerId { get; set; }

    }

    public enum TodoTaskStatus
    {
        Done = 1,
        Faraway = 2, // over 90 days
        Approching = 3, // 30 days to 90 days
        DueSoon = 4, // less than 30 days
        Due = 5
    }
}
