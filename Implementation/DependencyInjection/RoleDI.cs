using Application.Commands;
using Application.Queries;
using Implementation.Commands;
using Implementation.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Implementation.DependencyInjection
{
    public static class RoleDI
    {
        public static void AddRoleUseCases(this IServiceCollection services)
        {
            services.AddTransient<IGetRoleQuery, GetRole>();
            services.AddTransient<IGetRolesQuery, GetRoles>();
            services.AddTransient<IUpdateRoleCommand, UpdateRole>();
            services.AddTransient<ICreateRole, CreateRole>();
            services.AddTransient<IDeleteRoleCommand, DeleteRole>();
        }
    }
}
