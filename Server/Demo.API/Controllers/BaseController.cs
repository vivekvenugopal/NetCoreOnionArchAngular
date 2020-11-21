using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Demo.Common.ErrorHandling;

namespace Demo.API.Controllers
{  
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public abstract class BaseController:  Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                var controllerName = controllerActionDescriptor.ControllerName;
                var actionName = controllerActionDescriptor.ActionName;

                if(string.IsNullOrEmpty(controllerName) || string.IsNullOrEmpty(actionName))
                     throw new DemoAPIException("Invalid Request: Invalid Controller or action name");
                     
            }
            else
                throw new DemoAPIException("Invalid Request: Invalid Controller or action name");

        }
       

    }

}
