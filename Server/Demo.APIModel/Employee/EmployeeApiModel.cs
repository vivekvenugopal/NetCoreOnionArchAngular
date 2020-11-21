using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Demo.APIModel {
    public class EmployeeAPIModel {
        public long Id { get; set; }
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [JsonProperty(PropertyName = "DOJ")]
        public DateTime DateOfJoining { get; set; }
        public string Address { get; set; }
        public DateTime? DOB { get; set; }
        public List<SkillAPIModel> Skills { get; set; }
    }
    
}