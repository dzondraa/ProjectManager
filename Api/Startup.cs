using Application;
using Application.Commands;
using Application.Queries;
using AutoMapper;
using Implementation.Commands;
using Implementation.Implementation;
using Implementation.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Api.Filters;
using Api.Validations;
using Azure.Storage.Blobs;
using Api.Core;
using Implementation.Validatiors;
using EFDataAccess;
using Microsoft.EntityFrameworkCore;
using Implementation.DependencyInjection;
using static EFDataAccess.ProjectManagementContext;
using Api.Jwt;
using SocialNetwork.API.Jwt.TokenStorage;
using SocialNetwork.API.Jwt;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = new AppSettings();
            Configuration.Bind(appSettings);

            services.AddSingleton<ProjectManagementContextFactory>();
            //services.AddScoped<ProjectManagementContext>();

            services.AddTransient<UseCaseExecutor>();
            // Projects services
            services.AddTransient<ICreateProjectCommandAsync, TableCliCreateProject>();
            services.AddTransient<IDeleteProjectAsync, TableCliDeleteProjectAsync>();
            services.AddTransient<IQueryProject, TableCliQueryProject>();
            services.AddTransient<IGetProject, LinqGetProject>();
            services.AddTransient<IUpdateProjectCommandAsync, TableCliUpdateProjectAsync>();
            // Tasks services
            services.AddTransient<IQueryTask, TableCliQueryTask>();
            //services.AddTransient<IGetTask, LinqGetTask>();//ICreateTaskCommandAsync
            services.AddTransient<ICreateTaskCommandAsync, TableCliCreateTaskAsync>();
            services.AddTransient<IUpdateTaskAsync, TableCliUpdateTaskAsync>();
            services.AddTransient<IDeleteTaskAsync, TableCliDeleteTaskAsync>();
            
            // USE CASES
            services.AddUserUseCases();
            services.AddRoleUseCases();
            services.AddWorkItemUseCases();
            services.AddCommentsUseCases();

            // Files
            services.AddTransient<IGetCode, BlobCliGetCodeFiles>();
            services.AddTransient<IUploadFileCommandAsync, BlobCliUploadFile>();
            // Validations
            services.AddTransient<ProjectRequestValidator>();
            services.AddTransient<TaskRequestValidatior>();
            services.AddTransient<FileUploadValidator>();
            services.AddTransient<CreateUserRequestValidator>();
            services.AddTransient<UpdateUserRequestValidator>();
            services.AddTransient<CreateWorkItemValidator>();
            services.AddTransient<UpdateWorkItemValidator>();
            services.AddTransient<UpdateCommentValidation>();
            services.AddTransient<CreateCommentValidator>();



            services.AddTransient<IUseCaseLogger, DbUseCaseLogger>();
            services.AddSingleton(x => new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=eduaccount;AccountKey=1aum0Jx/fz/xENwYqz+j7JRTnYS5cIsUUdfZ1XvQ2R7NnoIaObJ7bg4KxInTt1IlvISRKOebtBSrroUEl43AZA==;EndpointSuffix=core.windows.net"));

            services.AddAutoMapper(typeof(Startup));
            services
                .AddControllers(/*optrions => optrions.Filters.Add<ValidationFilter>()*/);
            //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            // AUTH
            services.AddHttpContextAccessor();
            services.AddScoped<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var header = accessor.HttpContext.Request.Headers["Authorization"];

                var data = header.ToString().Split("Bearer ");

                if (data.Length < 2)
                {
                    return new AnonymusActor();
                }

                var handler = new JwtSecurityTokenHandler();

                var tokenObj = handler.ReadJwtToken(data[1].ToString());

                var claims = tokenObj.Claims;

                var email = claims.First(x => x.Type == "Email").Value;
                var id = claims.First(x => x.Type == "Id").Value;
                var username = claims.First(x => x.Type == "Username").Value;
                var roles = claims.Where(x => x.Type == "role").ToList();

                var roleIds = roles.Select(x => JsonConvert.DeserializeObject<string>(x.Value));

                return new JwtActor
                {
                    Email = email,
                    Roles = roleIds,
                    Id = int.Parse(id),
                    Username = username,
                };
            });
            services.AddJwt(appSettings);
            services.AddTransient<ITokenStorage, InMemoryTokenStorage>();
            services.AddTransient<JwtManager>(x =>
            {
                var contextFactoy = x.GetService<ProjectManagementContextFactory>();
                var tokenStorage = x.GetService<ITokenStorage>();
                return new JwtManager(contextFactoy, appSettings.Jwt.Issuer, appSettings.Jwt.SecretKey, appSettings.Jwt.DurationSeconds, tokenStorage);
            });

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Coding Projects Management API",
                    Description = "Open API for accessing the system to manage projects with code snippets"
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coding Projects Management API");
            });

            app.UseRouting();
            app.UseMiddleware<GlobalExceptionHandler>();



            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
