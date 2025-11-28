using Contracts.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagementContext.Application.DTOs;
using UserManagementContext.Application.Interfaces;
using UserManagementContext.Domain.Entities;

namespace UserManagementContext.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<UserEntity> _passwordHasher;

        public UserService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<UserEntity>();
        }

        public async Task<UserDto> CreateUserAsync(CreateUserRequest dto)
        {
            var user = new UserEntity
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = _passwordHasher.HashPassword(null!, dto.Password)
            };

            await _userRepository.AddAsync(user);

            return new UserDto
            {
                Username = user.UserName,
                Email = user.Email
            };
        }

        public async Task<UserDto?> LoginAsync(UserLoginRequest request)
        {
            var user = await _userRepository.GetByUsernameAsync(request.UserName);

            if (user == null) return null;
            var verification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (verification == PasswordVerificationResult.Failed)
                return null;

            var token = CreateToken(user);

            return new UserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = token
            };
        }

        private string CreateToken(UserEntity user)
        {
            var secret = Environment.GetEnvironmentVariable("JWT_SECRET")
            ?? throw new Exception("JWT_SECRET enviroment variable not set.");

            Console.WriteLine($"JWT SECRET FOUND: {secret}");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role) //Added for future role implementation
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: _configuration["AppSettings:Issuer"],
                audience: _configuration["AppSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}