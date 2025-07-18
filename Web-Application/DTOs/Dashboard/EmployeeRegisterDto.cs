namespace Web_Application.DTOs.Dashboard;

public class EmployeeRegisterDto
{
    public record EmployeeRegisterResponse(DateTime? registerDate, EmployeeDto.EmployeeResponse employeeRegister, EmployeeDto.EmployeeResponse registerBy, string description);
}
