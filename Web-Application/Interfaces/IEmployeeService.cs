using Web_Application.DTOs;

namespace Web_Application.Interfaces;

public interface IEmployeeService
{
    public Task<EmployeeDto?> RegisterEmployee(string idEmployee, EmployeeDto employeeDto);
    public Task<EmployeeDto?> DeleteEmployee(string idEmployee, EmployeeDto employeeDto);
    public Task<EmployeeDto?> UpdateEmployee(string idEmployee, EmployeeDto employeeDto);
    public Task<bool> SetValueRule(string idEmployee, double valueRule);
}
