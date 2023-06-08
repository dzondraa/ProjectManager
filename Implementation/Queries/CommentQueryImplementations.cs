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
    public class GetCommentsQuery : IGetCommentsQuery
    {
        private readonly ProjectManagementContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public GetCommentsQuery(IMapper mapper, ProjectManagementContextFactory dbContextFactory)
        {
            _mapper = mapper;
            _dbContextFactory = dbContextFactory;
        }

        public int Id => 55501;

        public string Name => "GET Comments";

        public PagedResponse<CommentDto> Execute(CommentSearch search)
        {
            var database = _dbContextFactory.CreateDbContext(null);
            var query = database.Comments
                .Include(x => x.User)
                .Include(x => x.Parent)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.Content))
                query = query.Where(x => x.Content.ToLower() == search.Content.ToLower());

            if (search.ParentID.HasValue)
                query = query.Where(x => x.Parent.Id == search.ParentID);

            if (search.UserId.HasValue)
                query = query.Where(x => x.User.Id == search.UserId);

            return query.ToPagedResponse<CommentDto, Comment>(search, _mapper);

        }
    }

    public class GetCommentQuery : IGetCommentQuery
    {
        private readonly ProjectManagementContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public GetCommentQuery(IMapper mapper, ProjectManagementContextFactory dbContextFactory)
        {
            _mapper = mapper;
            _dbContextFactory = dbContextFactory;
        }

        public int Id => 55502;

        public string Name => "GET Comment";

        public CommentDto Execute(int id)
        {
            var database = _dbContextFactory.CreateDbContext(null);
            var data = database.Comments
                .Include(x => x.User)
                .Include(x => x.Parent)
                .FirstOrDefault( x => x.Id == id);

            return _mapper.Map<CommentDto>(data);

        }
    }

}
