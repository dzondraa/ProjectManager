using Application.Requests;

namespace Application.Commands
{
    public interface ICreateWorkItem : ICommandAsync<CreateWorkItemRequest> { }

    public interface IUpdateWorkItemCommand : ICommandAsync<UpdateWorkItemRequest> { }

    public interface IDeleteWorkItemCommand : ICommandAsync<int> { }
}
