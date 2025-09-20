using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementContext.Domain.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;

        // Reference til Member i ClubContext (kun ID her, relation håndteres cross-context)
        public int MemberId { get; set; }
    }
}
