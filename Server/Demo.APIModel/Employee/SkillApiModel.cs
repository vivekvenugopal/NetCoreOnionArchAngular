using System;

namespace Demo.APIModel {
    public class SkillAPIModel {
        public long Id { get; set; }
        public string Technology { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}