namespace Demo.Model {
    public class UserRegistration : ModelBase {
        public string UserName{get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
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
         public int Activated { get; set; }
        public bool IsActivated {
            set {
                if (value == true)
                    Activated = 1;
                else
                    Activated = 0;
            }
            get {
                return (Activated == 1) ? true : false;
            }
        }

    }
}