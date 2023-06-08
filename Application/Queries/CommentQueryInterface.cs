using Application.DataTransfer;
using Application.Searches;

namespace Application.Queries
{
    public interface IGetCommentsQuery : IQuery<CommentSearch, PagedResponse<CommentDto>> { }

    public interface IGetCommentQuery : IQuery<int, CommentDto> { }
}
