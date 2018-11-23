using System;
using System.Collections.Generic;
using SmallSimpleOA.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace SmallSimpleOA.Models
{
    public partial class Uzer
    {

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }
        public bool? Valid { get; set; } = true;
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int? Department { get; set; }
        public int? UzerLevel { get; set; } //-1: super user; 0: system; other: normal user;
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime? LastLogin { get; set; }

        [InverseProperty("Applicant")]
        public virtual ICollection<AskForLeave> AskForLeaves { get; set; }

        [InverseProperty("CurrentAt")]
        public virtual ICollection<AskForLeave> LeaveRequests { get; set; }
    }
}
