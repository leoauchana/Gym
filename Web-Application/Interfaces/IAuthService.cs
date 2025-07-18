using Web_Application.DTOs;

namespace Web_Application.Interfaces;

public interface IAuthService
{
    public Task<UserDto.UserResponseAuthenticated?> LoginEmployee(UserDto.UserRequest? userDto);
}
