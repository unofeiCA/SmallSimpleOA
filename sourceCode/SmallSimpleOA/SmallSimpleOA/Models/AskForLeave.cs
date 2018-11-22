using System;
using System.Collections.Generic;
using SmallSimpleOA.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmallSimpleOA.Models
{
    public partial class AskForLeave
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }
        public bool? Valid { get; set; } = true;
        public int? Status { get; set; }

        [Column("Applicant")]
        public int ApplicantId { get; set; }
        public string Reason { get; set; }
        public DateTime? AppTime { get; set; }

        [Column("CurrentAt")]
        public int CurrentAtId { get; set; }
        public string Memo { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        [ForeignKey("ApplicantId")]
        public Uzer Applicant { get; set; }

        [ForeignKey("CurrentAtId")]
        public Uzer CurrentAt { get; set; }
    }

    public enum AskForLeaveStatus
    {
        Applied = 1,
        Reviewing = 2,
        Approved = 3
    }
}
