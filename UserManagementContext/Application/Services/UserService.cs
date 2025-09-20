using UserManagementContext.Application.DTOs;
using UserManagementContext.Application.Interfaces;

namespace UserManagementContext.Application.Services
{
    public class UserService : IUserService
    {
        public Task<AuthResult> AuthenticateAsync(LoginDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto?> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto?> GetByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto?> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> RegisterAsync(RegisterUserDto dto)
        {
            throw new NotImplementedException();
        }

        public Task RequestPasswordResetAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task ResetPasswordAsync(string resetToken, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEmailAsync(int userId, string newEmail)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
