using System;
using System.Collections.Generic;
using System.Text;
using Demo.APIModel;
using Demo.Business.InfraStructure;
using Demo.Model;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Demo.Common.ErrorHandling;

namespace Demo.Business.Service
{
    public class UserService : PersistenceService, IUserService
    {
        private IRepository<User> _userRepository;
        private IUnitOfWork _unitOfWork;
        private IAuthenticationService _authService;
        public IRepository<UserRegistration> _userRegisterRepo { get; set; }
        public UserService(IRepository<User> userRepository, IUnitOfWork unitOfWork,  IAuthenticationService authService,
        IRepository<UserRegistration> userRegisterRepo  )
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _authService = authService;
            _userRegisterRepo = userRegisterRepo;
        }

        public UserAPIModel Authenticate(string username, string password)
        {
            var userAPIModel = new UserAPIModel();

            if(_authService.IsValidUser(username, password))
            {
                var user = _userRepository.SelectOne(x => x.UserName == username);
                userAPIModel.Username = username;
                userAPIModel.FirstName = user.FirstName;
                userAPIModel.LastName = user.LastName;
                userAPIModel.Token =  _authService.GenerateToken(username);
                return userAPIModel;
            }
            else
                throw new DemoAPIException("The username and password combination is invalid.");
          
        }

        public bool IsValidUser(string userName)
        {
            var userRegistered = _userRegisterRepo.SelectOne(x=>x.UserName == userName.Trim());
            if(userRegistered != null && userRegistered.IsActivated)
                throw new DemoAPIException("This user is already registered with us. Please contact your administrator");
            else if (userRegistered != null && userRegistered.IsActivated == false)
                throw new DemoAPIException("This user is not yet activated. Please contact your administrator");
           return true;     
        }
        public void Register(UserAPIModel user)
        {
            if(user.IsInternal && IsValidUser(user.Username))
            {
                var registerUser = _authService.GetADUserDetails(user.Username);
                UserRegistration userRegister  =new UserRegistration();
                userRegister.FirstName =  registerUser.FirstName;
                userRegister.LastName = registerUser.LastName;
                userRegister.Email = registerUser.Email;
                userRegister.UserName = registerUser.UserName;
                userRegister.IsInternalUser = registerUser.IsInternalUser;
                userRegister.IsActivated = false;
                _userRegisterRepo.Add(userRegister);
                SaveChanges (_unitOfWork);
            }
        }
        public void ActivateUser(long id)
        {
            var userRegister =  _userRegisterRepo.SelectById(id);
            userRegister.IsActivated = true;
            
            _userRepository.Add(new User{
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                Email = userRegister.Email,
                UserName = userRegister.UserName,
                IsInternalUser = userRegister.IsInternalUser
            });
            _userRegisterRepo.Update(userRegister);
            SaveChanges(_unitOfWork);
        }
        public void DeativateUser(long id)
        {
            var userRegister =  _userRegisterRepo.SelectById(id);
            userRegister.IsActivated = false;    
            _userRegisterRepo.Update(userRegister);
            var user = _userRepository.SelectOne(x=>x.UserName == userRegister.UserName);
            if(user != null)
            {
                user.IsActive = false;
                _userRepository.Update(user);
            }
            SaveChanges(_unitOfWork);
        }
        public IEnumerable<UserRegistrationAPIModel> GetAllUserRegistrations()
        {
            var userRegistrations = _userRegisterRepo.SelectAll();
            List<UserRegistrationAPIModel> userRegAPI = new List<UserRegistrationAPIModel>();
            foreach(var userRegistration in userRegistrations)
            {
                userRegAPI.Add(new UserRegistrationAPIModel{
                    FirstName = userRegistration.FirstName,
                    LastName = userRegistration.LastName,
                    Email = userRegistration.Email,
                    Id = userRegistration.Id,
                    IsActivated = userRegistration.IsActivated,
                    Username = userRegistration.UserName
                });
            }
            return userRegAPI;
        }
       
    }
}
