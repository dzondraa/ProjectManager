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

            services.AddTransient<IUseCaseLogger, ConsoleUseCaseLogger>();

            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
