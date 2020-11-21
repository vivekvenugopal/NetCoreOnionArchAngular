using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Demo.APIModel;

namespace Demo.API.Controllers.User
{
    public class UserController : BaseController
    {
        private IUserService _userService;
        private IAuthenticationService _authenticationService;
        public UserController(IUserService userService, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate(UserAPIModel userData )
        {
             var user = _userService.Authenticate(userData.Username, userData.Password);
             return Ok(user);
        }
        [AllowAnonymous]
        [HttpPost("SetPassword")]
        public IActionResult SetPassword(UserAPIModel userData )
        {
            _authenticationService.SetUserPassWord(userData.Username, userData.Password);
             return Ok();
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(UserAPIModel userData )
        {
            _userService.Register(userData);
             return Ok();
        }
        [HttpGet("UserRegistrations")]
        public IEnumerable<UserRegistrationAPIModel> UserRegistrations()
        {
            return _userService.GetAllUserRegistrations();
        }
        [HttpPost("ActivateUser")]
        public IActionResult ActivateUser([FromBody] int RegistrationId)
        {
            _userService.ActivateUser(RegistrationId);
            return Ok();
        }
         [HttpPost("DeActivateUser")]
        public IActionResult DeActivateUser([FromBody] int RegistrationId)
        {
            _userService.DeativateUser(RegistrationId);
            return Ok();
        }
    }
}
