using System;
using System.Collections.Generic;

namespace SmallSimpleOA.Models
{
    public partial class Attendance
    {
        public int Id { get; set; }
        public bool? Valid { get; set; } = true;
        public int? UzerId { get; set; }
        public DateTime? ActionTime { get; set; }
        public int? ActionType { get; set; }

    }

    public enum AttendanceType
    {
        In = 1,
        Out = 2
    }
}
