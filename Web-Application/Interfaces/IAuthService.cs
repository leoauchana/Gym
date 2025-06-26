using Web_Application.DTOs;

namespace Web_Application.Interfaces;

public interface IAuthService
{
    public Task<EmployeeDto?> LoginEmployee(EmployeeDto employeeDto);
    public Task<EmployeeDto?> LogoutEmployee();
    public void TokenGenerator();
}
