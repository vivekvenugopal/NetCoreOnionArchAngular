namespace Demo.Model {
    public class UserPassword :ModelBase {
        public long UserId { get; set; }
        public string Password { get; set; }
        public string HashSalt { get; set; }
        public User User { get; set; }
    }
}