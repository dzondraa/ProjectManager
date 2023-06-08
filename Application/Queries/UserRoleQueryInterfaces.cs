using Application.DataTransfer;
using Application.Searches;

namespace Application.Queries
{
    public interface IGetRolesQuery : IQuery<RoleSearch, PagedResponse<RoleDto>> { }

    public interface IGetRoleQuery : IQuery<int, RoleDto> { }
}
