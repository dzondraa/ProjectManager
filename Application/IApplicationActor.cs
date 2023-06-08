using System.Collections.Generic;

namespace Application
{
    public interface IApplicationActor
    {
        int Id { get; }
        string Email { get; }
        string Username { get; }
        IEnumerable<string> Roles { get; }
    }
}
