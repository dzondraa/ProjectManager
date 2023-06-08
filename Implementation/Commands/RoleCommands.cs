using Application.Commands;
using Application.Requests;
using static EFDataAccess.ProjectManagementContext;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Exceptions;
using System.Linq;
using Implementation.Validatiors;
using Microsoft.Azure.Cosmos.Table.Queryable;

namespace Implementation.Commands
{
    public class CreateRole : ICreateRole
    {
        private readonly ProjectManagementContextFactory _projectManagementContextFactory;

        public CreateRole(ProjectManagementContextFactory projectManagementContextFactory)
        {
            _projectManagementContextFactory = projectManagementContextFactory;
        }

        public int Id => 33301;
        public string Name => "Create role";

        public async Task Execute(CreateRoleRequest request)
        {
            var database = _projectManagementContextFactory.CreateDbContext(null);

            var newRole = new Role
            {
                Name = request.Name,
            };

            database.Roles.Add(newRole);
            await database.SaveChangesAsync();
        }
    }

    public class UpdateRole : IUpdateRoleCommand
    {
        private readonly ProjectManagementContextFactory _projectManagementContextFactory;

        public UpdateRole(ProjectManagementContextFactory projectManagementContextFactory)
        {
            _projectManagementContextFactory = projectManagementContextFactory;
        }

        public int Id => 33302;
        public string Name => nameof(UpdateRole);

        public async Task Execute(UpdateRoleRequest request)
        {
            var database = _projectManagementContextFactory.CreateDbContext(null);

            var targetUser = database.Roles.FirstOrDefault(x => x.Id == request.Id);

            if (targetUser is null)
                throw new EntityNotFoundException(nameof(database.Roles));

            if (!string.IsNullOrEmpty(request.Name))
            {
                targetUser.Name = request.Name;
            }

            await database.SaveChangesAsync();
        }
    }

    public class DeleteRole : IDeleteRoleCommand
    {
        private readonly ProjectManagementContextFactory _projectManagementContextFactory;
        private readonly UpdateUserRequestValidator _validator;

        public DeleteRole(ProjectManagementContextFactory projectManagementContextFactory, UpdateUserRequestValidator validator)
        {
            _projectManagementContextFactory = projectManagementContextFactory;
            _validator = validator;
        }

        public int Id => 33323;
        public string Name => "Delete role";

        public async Task Execute(int id)
        {
            var database = _projectManagementContextFactory.CreateDbContext(null);
            var role = database.Roles.FirstOrDefault(u => u.Id == id);

            database.Roles.Remove(role);

            await database.SaveChangesAsync();

        }
    }
}
