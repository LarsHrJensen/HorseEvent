using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementContext.Application.DTOs
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public string? Token { get; set; }  // JWT eller session token
        public UserDto? User { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();
    }
}
