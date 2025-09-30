using Contracts.User;
using System.CodeDom.Compiler;
using System.Security.Cryptography;
using System.Text;
using UserManagementContext.Application.DTOs;
using UserManagementContext.Application.Interfaces;
using UserManagementContext.Domain.Entities;

namespace UserManagementContext.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDto> CreateUserAsync(CreateUserRequest dto)
        {
            // Lav nyt domæne-objekt
            var userEntity = new UserEntity();

            userEntity.Username = dto.UserName;
            userEntity.Salt = GenerateSalt();
            userEntity.PasswordHash = HashPasswordWithSaltAndPepper(dto.Password, userEntity.Salt);
            userEntity.Email = dto.Email;

            // Gem i repository
            await _userRepository.AddAsync(userEntity);

            // Map til DTO
            var userDto = new UserDto
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                Email = userEntity.Email
            };

            return userDto;
        }

        private string GenerateSalt(int size = 16)
        {
            byte[] salt = new byte[size];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        //TODO SIKKERHED: Implementer en sikker hashing-algoritme med salt og pepper 
        public string HashPasswordWithSaltAndPepper(string password, string salt)
        {
            string saltedAndPepperedPassword = password + salt; /*+ SecretPepper;*/


            byte[] passwordBytes = Encoding.UTF8.GetBytes(saltedAndPepperedPassword);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                // Convert to a readable hexadecimal string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
    
}
