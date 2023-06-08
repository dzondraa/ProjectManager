using Application;
using System.Collections.Generic;

namespace Api.Jwt
{
    public class JwtActor : IApplicationActor
    {
        public int Id { get; set;}

        public string Email { get; set; }

        public string Username { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }

    public class AnonymusActor : IApplicationActor
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public AnonymusActor() { 
            Id = 0;
            Email = "Anonymus";
            Username = "Anonymus";
            Roles = new List<string>();
        }
    }

}
