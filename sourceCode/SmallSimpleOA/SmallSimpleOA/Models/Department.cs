using System;
using System.Collections.Generic;

namespace SmallSimpleOA.Models
{
    public partial class Department
    {
        public int Id { get; set; }
        public bool? Valid { get; set; }
        public string Name { get; set; }
        public int? TotalStaffLevel { get; set; }
    }
}
