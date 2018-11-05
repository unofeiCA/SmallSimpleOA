using System;
using System.Collections.Generic;

namespace SmallSimpleOA.Models
{
    public partial class AskForLeave
    {
        public int Id { get; set; }
        public bool? Valid { get; set; } = true;
        public int? Status { get; set; }
        public int? Applicant { get; set; }
        public string Reason { get; set; }
        public DateTime? AppTime { get; set; }
        public int? CurrentAt { get; set; }
    }
}
