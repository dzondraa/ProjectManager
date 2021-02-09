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
            services.AddTransient<UseCaseExecutor>();
            // Projects services
            services.AddTransient<ICreateProjectCommandAsync, TableCliCreateProject>();
            services.AddTransient<IDeleteProjectAsync, TableCliDeleteProjectAsync>();
            services.AddTransient<IQueryProject, TableCliQueryProject>();
            services.AddTransient<IGetProject, LinqGetProject>();
            services.AddTransient<IUpdateProjectCommandAsync, TableCliUpdateProjectAsync>();
            // Tasks services
            services.AddTransient<IQueryTask, TableCliQueryTask>();
            services.AddTransient<IGetTask, LinqGetTask>();//ICreateTaskCommandAsync
            services.AddTransient<ICreateTaskCommandAsync, TableCliCreateTaskAsync>();
            services.AddTransient<IUpdateTaskAsync, TableCliUpdateTaskAsync>();
            services.AddTransient<IDeleteTaskAsync, TableCliDeleteTaskAsync>();
            // Files
            services.AddTransient<IGetCode, BlobCliGetCodeFiles>();
            services.AddTransient<IUploadFileCommandAsync, BlobCliUploadFile>();
            // Validations
            services.AddTransient<ProjectRequestValidator>();

            services.AddTransient<IUseCaseLogger, ConsoleUseCaseLogger>();
            services.AddSingleton(x => new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=eduaccount;AccountKey=1aum0Jx/fz/xENwYqz+j7JRTnYS5cIsUUdfZ1XvQ2R7NnoIaObJ7bg4KxInTt1IlvISRKOebtBSrroUEl43AZA==;EndpointSuffix=core.windows.net"));

            services.AddAutoMapper(typeof(Startup));
            services
                .AddControllers(/*optrions => optrions.Filters.Add<ValidationFilter>()*/);
                //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

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


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
