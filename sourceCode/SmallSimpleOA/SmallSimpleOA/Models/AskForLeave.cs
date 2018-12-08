using System;
using System.Collections.Generic;
using SmallSimpleOA.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmallSimpleOA.Models
{
    public partial class AskForLeave
    {
        public int Id { get; set; }
        public bool? Valid { get; set; } = true;
        public int? Status { get; set; }

        public string Reason { get; set; }
        public DateTime? AppTime { get; set; }

        public string Memo { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        [ForeignKey("ApplicantId")]
        public virtual Uzer Applicant { get; set; }

        [ForeignKey("CurrentAtId")]
        public virtual Uzer CurrentAt { get; set; }
    }

    public enum AskForLeaveStatus
    {
        Applied = 1,
        Reviewing = 2,
        Approved = 3,
        Rejected = 4
    }
}
