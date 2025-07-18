namespace Web_Application.DTOs;

public class UserDto
{
    public record UserRequest(string? userName, string? password);
    public record UserResponseAuthenticated(string? userName, string? name, string? lastName, string? gmail, int file,string? typeEmployee, string? token);
    public record UserResponseLog(string? name, string? lastName, string? gmail, int file, string? typeEmploye);
}
