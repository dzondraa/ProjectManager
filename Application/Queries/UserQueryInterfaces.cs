using Application.DataTransfer;
using Application.Searches;

namespace Application.Queries
{
    public interface IGetUsersQuery : IQuery<UserSearch, PagedResponse<UserDto>> { }
    
    public interface IGetSingleUsersQuery : IQuery<int, UserDto> { }

}
