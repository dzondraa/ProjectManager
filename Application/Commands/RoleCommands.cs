using Application.Requests;

namespace Application.Commands
{
    public interface ICreateRole : ICommandAsync<CreateRoleRequest> { }

    public interface IUpdateRoleCommand : ICommandAsync<UpdateRoleRequest> { }

    public interface IDeleteRoleCommand : ICommandAsync<int> { }
}
