using Web_Application.DTOs;
using Web_Domain.Entities;

namespace Web_Application.Interfaces;

public interface IAuthService
{
    public Task<(EmployeeDto?, string)> LoginEmployee(UserDto employeeDto);
    //public Task<UserDto?> LogoutEmployee();
}
