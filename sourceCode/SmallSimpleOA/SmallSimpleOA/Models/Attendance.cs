using System;
using System.Collections.Generic;

namespace SmallSimpleOA.Models
{
    public partial class Attendance
    {
        public int Id { get; set; }
        public bool? Valid { get; set; }
        public int? UzerId { get; set; }
        public bool? ActionType { get; set; }
        public DateTime? ActionTime { get; set; }
    }
}
