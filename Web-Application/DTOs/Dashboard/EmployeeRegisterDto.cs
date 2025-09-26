namespace Web_Application.DTOs.Dashboard;

public class EmployeeRegisterDto
{
    public record EmployeeRegisterResponse(DateTime? registerDate, EmployeeDto.EmployeeResponse employeeRegistered, EmployeeDto.EmployeeResponse registerBy, string description);
}
