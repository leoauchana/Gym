using Web_Application.DTOs;

namespace Web_Application.Interfaces;

public interface IEmployeeService
{
    public Task<EmployeeDto.Response?> RegisterEmployee(string idEmployee, EmployeeDto.Request employeeDto);
    public Task<EmployeeDto.Response?> DeleteEmployee(string idEmployee, Guid idEmployeeDelete);
    public Task<EmployeeDto.Response?> UpdateEmployee(string idEmployee, EmployeeDto.Request employeeDto);
    public Task<bool> SetValueRule(string idEmployee, double valueRule);
}
