namespace Demo.APIModel {
    public class TokenInfoAPIModel {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public string SecretKey { get; set; }
    }
}