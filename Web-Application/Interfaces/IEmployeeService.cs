using Web_Application.DTOs;
using static Web_Application.DTOs.RuleDto;

namespace Web_Application.Interfaces;

public interface IEmployeeService
{
    public Task<EmployeeDto.EmployeeResponse?> RegisterEmployee(string idEmployee, EmployeeDto.EmployeeRequest employeeDto);
    public Task<EmployeeDto.EmployeeResponse?> DeleteEmployee(string idEmployee, Guid idEmployeeDelete);
    public Task<EmployeeDto.EmployeeResponse?> UpdateEmployee(string idEmployee, EmployeeDto.EmployeeRequest employeeDto);
    public Task<List<EmployeeDto.EmployeeResponse>> GetAllEmployees();
    public Task<EmployeeDto.EmployeeResponse?> GetEmployeeById(Guid idEmployee);
    public Task<bool> SetValueRule(string idEmployee, RuleRequest valueRule);
}
