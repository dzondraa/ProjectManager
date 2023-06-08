using Application.Requests;

namespace Application.Commands
{
    public interface ICreateComment : ICommandAsync<CreateCommentRequest> { }

    public interface IUpdateComment : ICommandAsync<UpdateCommentRequest> { }

    public interface IDeleteComment : ICommandAsync<int> { }
}