using System.Collections.Generic;

namespace Data.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? StatusId { get; set; }


        public virtual Status Status { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
