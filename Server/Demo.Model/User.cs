using System;

namespace Demo.Model {
    public class User : ModelBase {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int SuperAdmin { get; set; }
        public bool IsSuperAdmin {
            set {
                if (value == true)
                    SuperAdmin = 1;
                else
                    SuperAdmin = 0;
            }
            get {
                return (SuperAdmin == 1) ? true : false;
            }
        }
        public int InternalUser { get; set; }
        public bool IsInternalUser {
            set {
                if (value == true)
                    InternalUser = 1;
                else
                    InternalUser = 0;
            }
            get {
                return (InternalUser == 1) ? true : false;
            }
        }
        public int ActiveUser { get; set; }
        public bool IsActive {
            set {
                if (value == true)
                    ActiveUser = 1;
                else
                    ActiveUser = 0;
            }
            get {
                return (ActiveUser == 1) ? true : false;
            }
        }
    }
}