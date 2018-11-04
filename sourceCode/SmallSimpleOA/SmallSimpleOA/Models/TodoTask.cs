using System;
using System.Collections.Generic;

namespace SmallSimpleOA.Models
{
    public partial class TodoTask
    {
        public int Id { get; set; }
        public bool? Valid { get; set; }
        public int? Status { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? ModifyTime { get; set; }
        public DateTime? DeadLine { get; set; }
        public int? UzerId { get; set; }

    }
}
