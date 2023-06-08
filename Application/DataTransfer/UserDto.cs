using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public virtual IEnumerable<RoleDto> UserRole { get; set; }
    }
}
