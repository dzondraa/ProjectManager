using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Api.Core
{
    public class GlobalIdValidator
    {
        private readonly RequestDelegate _next;

        public GlobalIdValidator(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if(
                (httpContext.Request.Method == "PUT" 
                || httpContext.Request.Method == "GET") 
                && httpContext.Request.RouteValues.ContainsKey("id")
            )
            {
                
                if(!httpContext.Request.RouteValues["id"].ToString().Contains("$"))
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return;
                }
                
               
                await _next(httpContext);
                return;
            }
            await _next(httpContext);
        }

    }
}
