using System.Collections.Generic;

namespace Core.Models
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public StatusDto Status { get; set; }

        public IEnumerable<int> PermissionIds { get; set; }
    }
}
