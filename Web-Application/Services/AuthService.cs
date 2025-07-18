using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web_Application.DTOs;
using Web_Application.Exceptions;
using Web_Application.Interfaces;
using Web_Domain.Entities;
using Web_Domain.Logs;
using Web_Domain.Repository;

namespace Web_Application.Services;

public class AuthService : IAuthService
{
    private readonly IRepository _repository;
    private readonly IConfiguration _configuration;
    public AuthService(IConfiguration configuration, IRepository repository)
    {
        _repository = repository;
        _configuration = configuration;
    }
    public async Task<UserDto.UserResponseAuthenticated?> LoginEmployee(UserDto.UserRequest? userDto)
    {
        var userFound = await _repository.ObtenerElPrimero<User>(
            u => u.UserName == userDto!.userName, nameof(Employee));
        if (userFound == null || !VerifyPassword(userDto!.password!, userFound.Password!)) throw new EntityNotFoundException("El usuario o la contraseña son incorrectos");
        if(userFound.Employee == null) throw new NullException("El usuario no tiene un empleado asociado");
        await RegisterLogin(userFound);
        return userFound != null ? new UserDto.UserResponseAuthenticated
        (
            userFound.UserName,
            userFound.Employee.Name,
            userFound.Employee.LastName,
            userFound.Employee.Email,
            userFound.Employee.File,
            userFound.Employee.TypeEmployee.ToString(),
            TokenGenerator(userFound)
        ) : null;
    }

    private string TokenGenerator(User user)
    {
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Employee!.Name!),
            new Claim(ClaimTypes.Role, user.Employee.TypeEmployee.ToString()!)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var jwtConfig = new JwtSecurityToken(
            claims: userClaims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: credentials
            );
        return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
    }
    private async Task RegisterLogin(User user)
    {
        var newLogin = new LogAccess 
        {
            Id = new Guid(),
            User = user,
            AccessDate = DateTime.Now,
            isSuccess = true,
        };
        await _repository.Agregar(newLogin);
    }
    private bool VerifyPassword(string passwordInput, string hashedPassword) => BCrypt.Net.BCrypt.Verify(passwordInput, hashedPassword);
}