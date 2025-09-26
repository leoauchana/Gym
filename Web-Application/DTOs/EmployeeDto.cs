using Web_Domain.Entities;

namespace Web_Application.DTOs;

public class EmployeeDto
{
    public record EmployeeRequest(string? name, string? lastName, string? gmail, TypeEmployee? typeEmployee, string? domicile, int age, string? userName, string? password, string description);
    public record EmployeeResponse(string? name, string? lastName, string? gmail, int file, string? typeEmployee);
}
