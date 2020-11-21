using System;

namespace Demo.Model
{
    public class Skill : ModelBase
    {
        public string Technology { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Employee Employee { get; set; }
    }
}
