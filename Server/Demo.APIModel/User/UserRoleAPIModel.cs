using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Demo.APIModel {
    public class UserRoleAPIModel {
      public long UserId{get;set;}
      public long AccountId { get; set; }
      public List<long> RoleIds { get; set; }
    }
}