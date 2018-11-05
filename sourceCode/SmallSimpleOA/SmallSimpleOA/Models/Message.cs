using System;
using System.Collections.Generic;

namespace SmallSimpleOA.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public bool? Valid { get; set; } = true;
        public int? MsgFrom { get; set; }
        public int? MsgTo { get; set; }
        public string Content { get; set; }
        public DateTime? MsgTime { get; set; }
    }
}
