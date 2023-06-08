using System.Collections.Generic;

namespace Application.Requests
{
    public class CreateUserRequest
    {
        public string UserName { get; set;}
        
        public string Password { get; set;}

        public string Email { get; set;}

        public List<int> RoleIds { get; set;}
    }

    public class UpdateUserRequest
    {
        public int UserId { get; set;}

        public string UserName { get; set; }

        public string Email { get; set; }

        public List<int> RoleIds { get; set; }
    }
}
