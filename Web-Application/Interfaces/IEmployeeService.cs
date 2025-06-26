using Web_Application.DTOs;

namespace Web_Application.Interfaces;

public interface IEmployeeService
{
    public Task<EmployeeDto> RegisterEmployee(EmployeeDto employeeDto);
    public Task<EmployeeDto> DeleteEmployee(EmployeeDto employeeDto);
    public Task<EmployeeDto> UpdateEmployee(EmployeeDto employeeDto);
}
