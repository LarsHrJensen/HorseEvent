using Microsoft.AspNetCore.Identity;

namespace UserManagementContext.Domain.Entities
{
    public class UserEntity : IdentityUser
    {
        public string Role { get; set; } = "User"; //Added for future role implementation
    }
}