using Application.DataTransfer;
using Application.Exceptions;
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
    public class GetUserEF : IGetUsersQuery
    {
        private readonly ProjectManagementContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public GetUserEF(ProjectManagementContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public int Id => 00101;

        public string Name => "Get users (EF Core)";
        public PagedResponse<UserDto> Execute(UserSearch search)
        {
            var query = _dbContextFactory.CreateDbContext(null)
                .Users.Include(x => x.UserRole)
                .ThenInclude(x => x.Role).AsQueryable();

            if(!string.IsNullOrEmpty(search.UserName))
                query = query.Where(user => user.Username.ToLower() == search.UserName.ToLower());
            
            if (!string.IsNullOrEmpty(search.Email))
                query = query.Where(user => user.Email.ToLower() == search.UserName.ToLower());

            return query.ToPagedResponse<UserDto, User>(search, _mapper);

        }
    }

    public class GetSingleUserEF : IGetSingleUsersQuery
    {
        private readonly ProjectManagementContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public GetSingleUserEF(ProjectManagementContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public int Id => 00102;

        public string Name => "Get user (EF Core)";

        public UserDto Execute(int id)
        {
            var user = _dbContextFactory.CreateDbContext(null)
                .Users.Include(x => x.UserRole)
                .ThenInclude(x => x.Role)
                .FirstOrDefault(u => u.Id == id);

            if (user is null)
                throw new EntityNotFoundException(id.ToString(), typeof(User));

            return _mapper.Map<UserDto>(user);
        }
    }
}
