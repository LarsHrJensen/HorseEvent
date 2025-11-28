namespace UserManagementContext.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } //Added for future role implementation
        public string? Token { get; set; }
        public int MemberId { get; set; }
    }
}