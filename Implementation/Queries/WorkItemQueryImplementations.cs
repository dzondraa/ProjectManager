using Application.DataTransfer;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using Domain.Entities;
using Implementation.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static EFDataAccess.ProjectManagementContext;

namespace Implementation.Queries
{
    public class GetWorkItems : IGetWorkItemsQuery
    {
        private readonly ProjectManagementContextFactory _projectManagementContextFactory;
        private readonly IMapper _mapper;

        public GetWorkItems(ProjectManagementContextFactory projectManagementContextFactory, IMapper mapper)
        {
            _projectManagementContextFactory = projectManagementContextFactory;
            _mapper = mapper;
        }

        public int Id => 44421;

        public string Name => "Get work items";

        public PagedResponse<WorkItemDto> Execute(WorkItemSearch search)
        {
            var database = _projectManagementContextFactory.CreateDbContext(null);
            var query = database.WorkItems.AsQueryable();

            query = query
                .Include(x => x.Project)
                .Include(x => x.WorkItemType)
                .Include(x => x.Attachments)
                .ThenInclude(x => x.File);

            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(wi => wi.Name == search.Name);
            }

            return query.ToPagedResponse<WorkItemDto, WorkItem>(search, _mapper);
        }
    }

    public class GetWorkItem : IGetWorkItemQuery
    {
        private readonly ProjectManagementContextFactory _projectManagementContextFactory;
        private readonly IMapper _mapper;

        public GetWorkItem(ProjectManagementContextFactory projectManagementContextFactory, IMapper mapper)
        {
            _projectManagementContextFactory = projectManagementContextFactory;
            _mapper = mapper;
        }

        public int Id => 44422;

        public string Name => "Get work item";

        public WorkItemDto Execute(int id)
        {
            var database = _projectManagementContextFactory.CreateDbContext(null);
            var workItem = database.WorkItems
                .Include(x => x.Project)
                .Include(x => x.WorkItemType)
                .Include(x => x.Attachments)
                .ThenInclude(x => x.File)
                .Where(x => x.Id == id)
                .ToList();

            return _mapper.Map<WorkItemDto>(workItem.FirstOrDefault());
        }
    }
}
