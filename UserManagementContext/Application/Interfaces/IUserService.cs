using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementContext.Application.DTOs;
using UserManagementContext.Application.DTOs;

namespace UserManagementContext.Application.Interfaces
{
    public interface IUserService
    {
        // Opretter en ny bruger (koblet til et eksisterende Member via MemberId)
        public Task<UserDto> RegisterAsync(RegisterUserDto registerDto);

        // Login med brugernavn/email + password
        Task<AuthResult> AuthenticateAsync(LoginDto dto);

        // Hent bruger
        Task<UserDto?> GetByIdAsync(int userId);
        Task<UserDto?> GetByUsernameAsync(string username);
        Task<UserDto?> GetByEmailAsync(string email);

        // Ændre credentials
        Task UpdateEmailAsync(int userId, string newEmail);
        Task UpdatePasswordAsync(int userId, string currentPassword, string newPassword);

        // Password reset flow
        Task RequestPasswordResetAsync(string email);
        Task ResetPasswordAsync(string resetToken, string newPassword);

        // Slet/deaktivér bruger
        Task DeleteAsync(int userId);
    }

}
