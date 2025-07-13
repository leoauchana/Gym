namespace Web_Application.DTOs;

public class UserDto
{
    public record Request(string? userName, string? password);
    public record Response(string? userName, string? name, string? lastName, string? gmail, int file,string? typeEmployee, string? token);
}
