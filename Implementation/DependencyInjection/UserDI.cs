using Application.Commands;
using Application.Queries;
using Implementation.Commands;
using Implementation.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Implementation.DependencyInjection
{
    public static class UserDI
    {
        public static void AddWorkItemUseCases(this IServiceCollection services)
        {
            services.AddScoped<IGetUsersQuery, GetUserEF>();
            services.AddScoped<IGetSingleUsersQuery, GetSingleUserEF>();
            services.AddTransient<ICreateUser, CreateUser>();
            services.AddTransient<IUpdateUserCommand, UpdateUser>();
            services.AddTransient<IDeleteUserCommand, DeleteUser>();
        }
    }
}
