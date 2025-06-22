using Web_Application.DTOs;
using Web_Application.Interfaces;
using Web_Domain.Entities;
using Web_Domain.Repository;

namespace Web_Application.Services;

public class AuthService : IAuthService
{
    private readonly IRepository _repository;
    public AuthService(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<EmployeeDto?> LoginEmployee(EmployeeDto employeeDto)
    {
        return null;
    }
    public async Task<EmployeeDto> LogoutEmployee()
    {
        return null;
    }
}
