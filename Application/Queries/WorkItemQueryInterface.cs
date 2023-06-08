using Application.DataTransfer;
using Application.Searches;

namespace Application.Queries
{
    public interface IGetWorkItemsQuery : IQuery<WorkItemSearch, PagedResponse<WorkItemDto>> { }

    public interface IGetWorkItemQuery : IQuery<int, WorkItemDto> { }
}
