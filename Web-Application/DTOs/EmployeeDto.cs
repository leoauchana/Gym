using Web_Domain.Entities;

namespace Web_Application.DTOs;

public class EmployeeDto
{
    public record Request(string? name, string? lastName, string? gmail, int file, TypeEmployee? typeEmployee, string? domicile, int age, string? userName, string? password);
    public record Response(string? name, string? lastName, string? gmail, int file, string? typeEmployee);
}
