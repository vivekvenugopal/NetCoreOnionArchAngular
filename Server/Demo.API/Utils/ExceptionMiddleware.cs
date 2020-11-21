using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Demo.APIModel;
using Demo.Business.Service;
using Demo.Common.ErrorHandling;
using Demo.Common.Logger;

namespace Demo.API.Utils 
{
    public class ExceptionMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;
        private readonly IHostingEnvironment _env;
        public ExceptionMiddleware (RequestDelegate next, ILoggerManager logger, IHostingEnvironment env) {
            _logger = logger;
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync (HttpContext httpContext) {
            try {
                await _next (httpContext);
            } catch (Exception ex) {
                _logger.LogError ($"Something went wrong: {ex}");
                await HandleExceptionAsync (httpContext, ex, _env);
            }
        }

        private static Task HandleExceptionAsync (HttpContext context, Exception exception, IHostingEnvironment envr) {
            context.Response.ContentType = "application/json";
            DemoAPIException utiliexcep = null;
            if(exception is DemoAPIException)
             utiliexcep  = (DemoAPIException)exception;
            var isDev = envr.IsDevelopment();

            if(utiliexcep == null)
            {
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                return context.Response.WriteAsync (new ErrorDetails () {
                    StatusCode = context.Response.StatusCode,
                    Message = isDev ?  exception.Message : "Internal Server Error.",
                    Description = isDev && exception.InnerException != null ? exception.InnerException.Message  : "Please refer log"
                }.ToString ());
            }
            else{
                context.Response.StatusCode = (int) HttpStatusCode.UnprocessableEntity;
                var results = utiliexcep.ValidationResult;
                if(results.Count == 1 )
                {
                    return context.Response.WriteAsync (JsonConvert.SerializeObject(new DemoValidationResult{
                        Message = results[0].Message,
                        FieldName = results[0].FieldName
                    }));
                }
                else
                    return context.Response.WriteAsync (JsonConvert.SerializeObject(results));
            }
        }
    }
}