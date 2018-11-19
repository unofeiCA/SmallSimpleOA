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
        public string Memo { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

    }

    public enum AskForLeaveStatus
    {
        Applied = 1,
        Reviewing = 2,
        Approved = 3
    }
}
