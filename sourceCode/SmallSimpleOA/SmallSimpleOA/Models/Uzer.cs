using System;
using System.Collections.Generic;

namespace SmallSimpleOA.Models
{
    public partial class Uzer
    {
        public int Id { get; set; }
        public bool? Valid { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int? Department { get; set; }
        public int? UzerLevel { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
