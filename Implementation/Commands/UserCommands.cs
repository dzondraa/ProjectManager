using Application.Commands;
using Application.Exceptions;
using Application.Requests;
using Domain.Entities;
using FluentValidation;
using Implementation.Core;
using Implementation.Validatiors;
using System.Linq;
using System.Threading.Tasks;
using static EFDataAccess.ProjectManagementContext;

namespace Implementation.Commands
{
    public class CreateUser : ICreateUser
    {
        private readonly ProjectManagementContextFactory _projectManagementContextFactory;
        private readonly CreateUserRequestValidator _validator;

        public CreateUser(ProjectManagementContextFactory projectManagementContextFactory, CreateUserRequestValidator validator)
        {
            _projectManagementContextFactory = projectManagementContextFactory;
            _validator = validator;
        }

        public int Id => 10021;
        public string Name => "Create user";

        public async Task Execute(CreateUserRequest request)
        {
            _validator.ValidateAndThrow(request);
            var database = _projectManagementContextFactory.CreateDbContext(null);

            var newUser = new User
            {
                Email = request.Email,
                Username = request.UserName,
                Password = Helper.EncodePasswordToBase64(request.Password),
            };

            database.Users.Add(newUser);
            await database.SaveChangesAsync();

            request.RoleIds.ForEach(roleId =>
            {
                database.UserRole.Add(new UserRole
                {
                    RoleId = roleId,
                    UserId = newUser.Id,
                });
            });
            await database.SaveChangesAsync();
        }
    }

    public class UpdateUser : IUpdateUserCommand
    {
        private readonly ProjectManagementContextFactory _projectManagementContextFactory;
        private readonly UpdateUserRequestValidator _validator;

        public UpdateUser(ProjectManagementContextFactory projectManagementContextFactory, UpdateUserRequestValidator validator)
        {
            _projectManagementContextFactory = projectManagementContextFactory;
            _validator = validator;
        }

        public int Id => 10022;
        public string Name => "Update user";

        public async Task Execute(UpdateUserRequest request)
        {
            var validationResult = _validator.Validate(request);
            var database = _projectManagementContextFactory.CreateDbContext(null);

            var targetUser = database.Users.FirstOrDefault(x => x.Id == request.UserId);

            if (targetUser is null)
                throw new EntityNotFoundException(nameof(database.Users));

            if (!(request.UserName is null) &&
                validationResult.Errors.Where(e => e.PropertyName == nameof(request.UserName)).Count() == 0)
            {
                targetUser.Username = request.UserName;
            }
            if (!(request.Email is null) &&
                validationResult.Errors.Where(e => e.PropertyName == nameof(request.Email)).Count() == 0)
            {
                targetUser.Email = request.Email;
            }

            await database.SaveChangesAsync();

        }

    }

    public class DeleteUser : IDeleteUserCommand
    {
        private readonly ProjectManagementContextFactory _projectManagementContextFactory;
        private readonly UpdateUserRequestValidator _validator;

        public DeleteUser(ProjectManagementContextFactory projectManagementContextFactory, UpdateUserRequestValidator validator)
        {
            _projectManagementContextFactory = projectManagementContextFactory;
            _validator = validator;
        }

        public int Id => 10023;
        public string Name => "Delete user";

        public async Task Execute(int id)
        {
            var database = _projectManagementContextFactory.CreateDbContext(null);
            var user = database.Users.FirstOrDefault(u => u.Id == id);

            database.Users.Remove(user);
            
            await database.SaveChangesAsync();

        }
    }
}