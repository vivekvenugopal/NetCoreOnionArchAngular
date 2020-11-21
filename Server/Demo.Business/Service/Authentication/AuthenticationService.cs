using System;
using System.DirectoryServices.AccountManagement;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Demo.APIModel;
using Demo.Business.InfraStructure;
using Demo.Common;
using Demo.Common.ErrorHandling;
using Demo.Model;

namespace Demo.Business.Service {
    public class AuthenticationService : PersistenceService, IAuthenticationService {
        private IRepository<User> _userRepository;
        private IRepository<UserPassword> _userPasswordRepository;
        private IConfiguration _configuration;
        private IUnitOfWork _unitOfWork;
        private readonly AppSettingsModel _appSettings;

        public AuthenticationService (IConfiguration configuration, IRepository<User> userRepository,
            IRepository<UserPassword> userPasswordRepository, IOptions<AppSettingsModel> appSettings, IUnitOfWork unitOfWork) {
            _userRepository = userRepository;
            _configuration = configuration;
            _userPasswordRepository = userPasswordRepository;
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
        }

        public string GenerateToken (string username) {
            var tokenInfo = _appSettings.TokenOptions;
            var secretKey = tokenInfo.SecretKey;
            var currentUser = _userRepository.SelectOne (x => x.UserName == username);
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler ();
            var key = Encoding.ASCII.GetBytes (secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Issuer = tokenInfo.Issuer,
                Audience = tokenInfo.Audience,
                Subject = new ClaimsIdentity (new Claim[] {
                new Claim (ClaimTypes.Name, Convert.ToString (currentUser.Id)),
                new Claim ("IsSuperAdmin", Convert.ToString (currentUser.IsSuperAdmin))
                }),
                Expires = DateTime.UtcNow.AddMinutes (tokenInfo.AccessTokenExpiration),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken (tokenDescriptor);
            string tokenString = tokenHandler.WriteToken (token);
            return tokenString;
        }
        public bool IsValidUser (string username, string password) {
            var user = _userRepository.SelectOne (x => x.UserName == username);
            if (user == null)
                return false;

            if (user.IsInternalUser) {
                using (PrincipalContext pc = new PrincipalContext (ContextType.Domain, "ustr.com")) {
                    byte[] data = Convert.FromBase64String (password);
                    string decodedPassword = Encoding.UTF8.GetString (data);
                    // validate the credentials
                    bool isValid = pc.ValidateCredentials (username, decodedPassword);
                    return isValid;
                }
            }

            var userPassword = _userPasswordRepository.SelectOne (x => x.UserId == user.Id);
            if (userPassword != null) {
                byte[] data = Convert.FromBase64String (password);
                string decodedPassword = Encoding.UTF8.GetString (data);

                var hasPassword = GetHash (decodedPassword + userPassword.HashSalt);
                if (hasPassword == userPassword.Password)
                    return true;
            }

            return false;
        }

        public void SetUserPassWord (string username, string password) {
            var user = _userRepository.SelectOne (x => x.UserName == username);
            if (user != null) {
                var salt = GetHashSalt ();
                var hasPassword = GetHash (password + salt);
                UserPassword userPassword = new UserPassword {
                    UserId = user.Id,
                    Password = hasPassword,
                    HashSalt = salt
                };
                _userPasswordRepository.Add (userPassword);
                SaveChanges (_unitOfWork);
            }
        }
        public User GetADUserDetails (string userName) {
            return SendTestUser(); // Comment this out when active directory is being used
            User user = new User ();
            using (var context = new PrincipalContext (ContextType.Domain, "your domin name")) {
                var ADUser = UserPrincipal.FindByIdentity (context, userName);
                if (ADUser != null) {
                    user.UserName = userName;
                    user.FirstName = ADUser.Name.Split (' ') [0];
                    user.LastName = ADUser.Name.Split (' ') [1];
                    user.Email = ADUser.EmailAddress;
                    user.IsInternalUser = true;
                } else
                    throw new DemoAPIException ("Please enter a valid employee Id.");
            }
            return user;
        }
        private User SendTestUser()
        {
            return new User{
                UserName ="TestUser",FirstName="Test User", 
                LastName="Test User", Email="Test@TestCompany",
                IsInternalUser =true
            };
        }
        private string GetHashSalt () {
            byte[] bytes = new byte[128 / 8];
            using (var keyGenerator = RandomNumberGenerator.Create ()) {
                keyGenerator.GetBytes (bytes);
                return BitConverter.ToString (bytes).Replace ("-", "").ToLower ();
            }
        }
        private string GetHash (string text) {
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create ()) {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash (Encoding.UTF8.GetBytes (text));
                // Get the hashed string.  
                return BitConverter.ToString (hashedBytes).Replace ("-", "").ToLower ();
            }
        }

    }
}