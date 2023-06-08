using Application.Commands;
using Application.Requests;
using Implementation.Core;
using Implementation.Validatiors;
using static EFDataAccess.ProjectManagementContext;
using System.Threading.Tasks;
using FluentValidation;
using Domain.Entities;
using System.Linq;
using Application.Exceptions;

namespace Implementation.Commands
{
    public class CreateWorkItem : ICreateWorkItem
    {
        private readonly ProjectManagementContextFactory _projectManagementContextFactory;
        private readonly CreateWorkItemValidator _validator;

        public CreateWorkItem(ProjectManagementContextFactory projectManagementContextFactory, CreateWorkItemValidator validator)
        {
            _projectManagementContextFactory = projectManagementContextFactory;
            _validator = validator;
        }

        public int Id => 44401;
        public string Name => "Create Work Item";

        public async Task Execute(CreateWorkItemRequest request)
        {
            _validator.ValidateAndThrow(request);
            var database = _projectManagementContextFactory.CreateDbContext(null);

            var newWorkItem = new WorkItem
            {
                Name = request.Name,
                Description = request.Description,
                Project = database.Projects.First(p => p.Id == request.ProjectId),
                WorkItemType = database.WorkItemTypes.First(w => w.Id == request.TypeId),
            };


            database.WorkItems.Add(newWorkItem);
            await database.SaveChangesAsync();
        }
    }

    public class UpdateWorkItem : IUpdateWorkItemCommand
    {
        private readonly ProjectManagementContextFactory _projectManagementContextFactory;
        private readonly UpdateWorkItemValidator _validator;

        public UpdateWorkItem(ProjectManagementContextFactory projectManagementContextFactory, UpdateWorkItemValidator validator)
        {
            _projectManagementContextFactory = projectManagementContextFactory;
            _validator = validator;
        }

        public int Id => 44402;
        public string Name => "Update Work Item";

        public async Task Execute(UpdateWorkItemRequest request)
        {
            _validator.ValidateAndThrow(request);
            var database = _projectManagementContextFactory.CreateDbContext(null);

            var targetWorkItem = database.WorkItems.FirstOrDefault(wi => wi.Id == request.Id);
            
            if(targetWorkItem == null)
            {
                throw new EntityNotFoundException(request.Id.ToString(), typeof(WorkItem));
            }

            if(!string.IsNullOrEmpty(request.Name))
            {
                targetWorkItem.Name = request.Name;
            }

            if (!(request.Description is null))
            {
                targetWorkItem.Description = request.Description;
            }

            if(request.TypeId.HasValue) 
            {
                targetWorkItem.WorkItemType = database.WorkItemTypes.FirstOrDefault(t => t.Id == request.TypeId);
            }

            if (request.ProjectId.HasValue)
            {
                targetWorkItem.Project = database.Projects.FirstOrDefault(x => x.Id == request.ProjectId);
            }

            await database.SaveChangesAsync();
        }
    }

    public class DeleteWorkItem : IDeleteWorkItemCommand
    {
        private readonly ProjectManagementContextFactory _projectManagementContextFactory;

        public DeleteWorkItem(ProjectManagementContextFactory projectManagementContextFactory)
        {
            _projectManagementContextFactory = projectManagementContextFactory;
        }

        public int Id => 44404;
        public string Name => "Delete work item";

        public async Task Execute(int id)
        {
            var database = _projectManagementContextFactory.CreateDbContext(null);
            var workItem = database.WorkItems.FirstOrDefault(x => x.Id == id);

            database.WorkItems.Remove(workItem);

            await database.SaveChangesAsync();

        }
    }
}
