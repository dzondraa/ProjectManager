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
            if(httpContext.Request.Method == "PUT")
            {
                var values = httpContext.Request.RouteValues;
                if(!values["id"].ToString().Contains("$"))
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }
            }
        }

    }
}
