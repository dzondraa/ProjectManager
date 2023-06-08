using Application.Commands;
using Application.Queries;
using Implementation.Commands;
using Implementation.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Implementation.DependencyInjection
{
    public static class CommentsDI
    {
        public static void AddCommentsUseCases(this IServiceCollection services)
        {
            services.AddTransient<IGetCommentsQuery, GetCommentsQuery>();
            services.AddTransient<IGetCommentQuery, GetCommentQuery>();
            services.AddTransient<ICreateComment, CreateComment>();
            services.AddTransient<IUpdateComment, UpdateComment>();
            services.AddTransient<IDeleteComment, DeleteComment>();
        }
    }
}
