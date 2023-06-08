using Application.Commands;
using Application.Exceptions;
using Application.Requests;
using Domain.Entities;
using Implementation.Validatiors;
using System;
using System.Collections.Generic;
using System.Text;
using static EFDataAccess.ProjectManagementContext;
using System.Threading.Tasks;
using FluentValidation;
using System.Linq;

namespace Implementation.Commands
{
    public class CreateComment : ICreateComment
    {
        private readonly ProjectManagementContextFactory _projectManagementContextFactory;
        private readonly CreateCommentValidator _validator;

        public CreateComment(ProjectManagementContextFactory projectManagementContextFactory, CreateCommentValidator validator)
        {
            _projectManagementContextFactory = projectManagementContextFactory;
            _validator = validator;
        }

        public int Id => 55511;
        public string Name => "Create Comment";

        public async Task Execute(CreateCommentRequest request)
        {
            _validator.ValidateAndThrow(request);
            var database = _projectManagementContextFactory.CreateDbContext(null);

            var newComment = new Comment
            {
                Content = request.Content,
                Parent = database.Comments.FirstOrDefault(p => p.Id == request.ParentId),
                User = database.Users.First(x => x.Id == request.UserId),
                WorkItem = database.WorkItems.First(x => x.Id == request.WorkItemId)
            };


            database.Comments.Add(newComment);
            await database.SaveChangesAsync();
        }
    }

    public class UpdateComment : IUpdateComment
    {
        private readonly ProjectManagementContextFactory _projectManagementContextFactory;
        private readonly UpdateCommentValidation _validator;

        public UpdateComment(ProjectManagementContextFactory projectManagementContextFactory, UpdateCommentValidation validator)
        {
            _projectManagementContextFactory = projectManagementContextFactory;
            _validator = validator;
        }

        public int Id => 55512;
        public string Name => "Update Comment";

        public async Task Execute(UpdateCommentRequest request)
        {
            _validator.ValidateAndThrow(request);
            var database = _projectManagementContextFactory.CreateDbContext(null);

            var targetComment = database.Comments.FirstOrDefault(wi => wi.Id == request.Id);

            if (targetComment == null)
            {
                throw new EntityNotFoundException(request.Id.ToString(), typeof(WorkItem));
            }

            if (!string.IsNullOrEmpty(request.Content))
            {
                targetComment.Content = request.Content;
            }

            if (request.ParentId.HasValue)
            {
                targetComment.Parent = database.Comments.FirstOrDefault(t => t.Id == request.ParentId);
            }

            if (request.UserId.HasValue)
            {
                targetComment.User = database.Users.FirstOrDefault(x => x.Id == request.UserId);
            }

            await database.SaveChangesAsync();
        }
    }

    public class DeleteComment : IDeleteComment
    {
        private readonly ProjectManagementContextFactory _projectManagementContextFactory;

        public DeleteComment(ProjectManagementContextFactory projectManagementContextFactory)
        {
            _projectManagementContextFactory = projectManagementContextFactory;
        }

        public int Id => 55513;
        public string Name => "Delete comment";

        public async Task Execute(int id)
        {
            var database = _projectManagementContextFactory.CreateDbContext(null);
            var comment = database.Comments.FirstOrDefault(x => x.Id == id);

            database.Comments.Remove(comment);

            await database.SaveChangesAsync();

        }
    }
}
