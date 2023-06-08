using Application.Requests;

namespace Application.Commands
{
    public interface ICreateUser : ICommandAsync<CreateUserRequest> { }
    
    public interface IUpdateUserCommand : ICommandAsync<UpdateUserRequest> { }
    
    public interface IDeleteUserCommand : ICommandAsync<int> { }
}
