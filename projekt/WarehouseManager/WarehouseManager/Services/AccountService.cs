using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WarehouseManager.Entites;
using WarehouseManager.Exceptions;
using WarehouseManager.Models;

namespace WarehouseManager.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
        public void Update(int userId, UpdateUserDto dto);
    }

    public class AccountService : IAccountService
    {
        private readonly WarehauseManagerDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        public AccountService(WarehauseManagerDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            if (dto.RoleId != 1)
            {
                throw new ForbidException("Wrong role");
            }

            var newUser = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Department = dto.Department,
                RoleId = dto.RoleId
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public void Update(int userId, UpdateUserDto dto)
        {
            var user = _context
                .Users
                .FirstOrDefault(u => u.Id == userId);
            if (user == null) throw new NotFoundException("User not found");
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.Department = dto.Department;
            user.RoleId = dto.RoleId;
            _context.SaveChanges();
        }

        public string GenerateJwt(LoginDto dto)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);
            if (user is null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
           if (result == PasswordVerificationResult.Failed)
           {
               throw new BadRequestException("Invalid username or password");
           }

           var claims = new List<Claim>()
           {
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
               new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
               new Claim("Department", user.Department)
           };

           var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
           var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
           var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

           var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
               _authenticationSettings.JwtIssuer,
               claims,
               expires: expires,
               signingCredentials: cred);

           var tokenHandler = new JwtSecurityTokenHandler();
           return tokenHandler.WriteToken(token);
        }
    }
}
