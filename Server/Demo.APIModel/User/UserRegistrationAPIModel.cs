using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Demo.APIModel {
    public class UserRegistrationAPIModel {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public bool IsInternal { get; set; }
        public string Email { get; set; }
        public bool IsActivated { get; set; }
    }
}