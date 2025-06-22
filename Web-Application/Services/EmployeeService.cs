using Web_Application.DTOs;
using Web_Application.Interfaces;
using Web_Domain.Entities;
using Web_Domain.Repository;

namespace Web_Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository _repository;
    public EmployeeService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<EmployeeDto?> RegisterEmployee(EmployeeDto employeeDto)
    {
        var employee = new Employee()
        {
            Id = new Guid,
            Name = employeeDto.Name,
            LastName = employeeDto.LastName,
            Age = employeeDto.Age,
            Gmail = employeeDto.Gmail,
            TypeEmployee = employeeDto.TypeEmployee,
            File = employeeDto.File,
            Domicile = employeeDto.Domicile,
        };
        await _repository.Agregar(employee);
        if (employeeDto == null) return null;
        return employeeDto;
    }
}
