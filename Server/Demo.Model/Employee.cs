using System;
using System.Collections.Generic;

namespace Demo.Model {
    public class Employee : ModelBase {
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfJoining {get;set;}
        public string Address { get; set; }
        public DateTime? DOB { get; set; }
        public ICollection<Skill> Skills { get; set; }

    }
}