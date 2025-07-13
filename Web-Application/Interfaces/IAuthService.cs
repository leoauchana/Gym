using Web_Application.DTOs;
using Web_Domain.Entities;

namespace Web_Application.Interfaces;

public interface IAuthService
{
    public Task<UserDto.Response?> LoginEmployee(UserDto.Request? userDto);
}
