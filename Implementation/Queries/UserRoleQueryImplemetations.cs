using Application.DataTransfer;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using Domain.Entities;
using Implementation.Core;
using System.Linq;
using static EFDataAccess.ProjectManagementContext;

namespace Implementation.Queries
{
    public class GetRoles : IGetRolesQuery
    {
        private readonly ProjectManagementContextFactory _projectManagementContextFactory;
        private readonly IMapper _mapper;

        public GetRoles(ProjectManagementContextFactory projectManagementContextFactory, IMapper mapper)
        {
            _projectManagementContextFactory = projectManagementContextFactory;
            _mapper = mapper;
        }

        public int Id => 22201;

        public string Name => "Get roles";

        public PagedResponse<RoleDto> Execute(RoleSearch search)
        {
            var database = _projectManagementContextFactory.CreateDbContext(null);
            var query = database.Roles.AsQueryable();

            if(!string.IsNullOrEmpty(search.Name)) 
            {
                query = query.Where(role => role.Name == search.Name);
            }

            return query.ToPagedResponse<RoleDto, Role>(search, _mapper);
        }
    }

    public class GetRole : IGetRoleQuery
    {
        private readonly ProjectManagementContextFactory _projectManagementContextFactory;
        private readonly IMapper _mapper;

        public GetRole(ProjectManagementContextFactory projectManagementContextFactory, IMapper mapper)
        {
            _projectManagementContextFactory = projectManagementContextFactory;
            _mapper = mapper;
        }

        public int Id => 22202;

        public string Name => "Get role";

        public RoleDto Execute(int id)
        {
            var database = _projectManagementContextFactory.CreateDbContext(null);
            var role = database.Roles.FirstOrDefault(role => role.Id == id);
            return _mapper.Map<RoleDto>(role);
        }
    }
}
