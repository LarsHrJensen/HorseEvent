using UserManagementContext.Application.DTOs;
using Contracts.User;

namespace UserManagementContext.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(CreateUserRequest dto);

    }

}
