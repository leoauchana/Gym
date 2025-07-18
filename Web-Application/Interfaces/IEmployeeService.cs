using Web_Application.DTOs;

namespace Web_Application.Interfaces;

public interface IEmployeeService
{
    public Task<EmployeeDto.EmployeeResponse?> RegisterEmployee(string idEmployee, EmployeeDto.EmployeeRequest employeeDto);
    public Task<EmployeeDto.EmployeeResponse?> DeleteEmployee(string idEmployee, Guid idEmployeeDelete);
    public Task<EmployeeDto.EmployeeResponse?> UpdateEmployee(string idEmployee, EmployeeDto.EmployeeRequest employeeDto);
    public Task<bool> SetValueRule(string idEmployee, double valueRule);
}
