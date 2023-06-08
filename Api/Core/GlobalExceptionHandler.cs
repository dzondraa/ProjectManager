using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Newtonsoft.Json;
using Application.Exceptions;
using FluentValidation;
using Microsoft.WindowsAzure.Storage;
using System.IO;

namespace Api.Core
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";
                object response = new {
                    message = ex.Message
                };
                var statusCode = StatusCodes.Status500InternalServerError;
                var type = ex.GetType();
                
                if(ex.Message.Contains("not exist"))
                {
                    statusCode = StatusCodes.Status404NotFound;
                    response = new
                    {
                        message = "Resource not found."
                    };

                }
                switch (ex)
                {
      
                    case EntityNotFoundException _:
                        statusCode = StatusCodes.Status404NotFound;
                        response = new
                        {
                            message = "Resource not found."
                        };
                        break;



                    case ValidationException validationException:
                        statusCode = StatusCodes.Status422UnprocessableEntity;


                        response = new
                        {
                            message = "Failed due to validation errors.",
                            errors = validationException.Errors.Select(x => new
                            {
                                x.PropertyName,
                                x.ErrorMessage
                            })
                        };
                        break;

                    case FileNotFoundException _:
                        statusCode = 400;
                        response = new
                        {
                            message = "Please provide a valid file path (.zip)"
                        };
                        break;

                    case UnauthorizedAccessException unauthorizedException:
                        statusCode = 401;
                        response = new {
                            message = unauthorizedException.Message
                        };
                        break;

                }

                httpContext.Response.StatusCode = statusCode;

                if (response != null)
                {
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
                    return;
                }

                await Task.FromResult(httpContext.Response);
            }
        }

    }
}
