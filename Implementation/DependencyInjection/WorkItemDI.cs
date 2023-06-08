using Application.Commands;
using Application.Queries;
using Implementation.Commands;
using Implementation.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Implementation.DependencyInjection
{
    public static class WorkItemDI
    {
        public static void AddUserUseCases(this IServiceCollection services)
        {
            services.AddScoped<IGetWorkItemsQuery, GetWorkItems>();
            services.AddScoped<IGetWorkItemQuery, GetWorkItem>();
            services.AddTransient<ICreateWorkItem, CreateWorkItem>();
            services.AddTransient<IUpdateWorkItemCommand, UpdateWorkItem>();
            services.AddTransient<IDeleteWorkItemCommand, DeleteWorkItem>();
        }
    }
}
