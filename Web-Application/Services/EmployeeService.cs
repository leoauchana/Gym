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
        var employees = await _repository.ListarTodos<Employee>();
        var employeeFound = employees.Where(e => e.Gmail == employeeDto.Gmail).FirstOrDefault();
        if (employeeFound == null) return null;
        var employee = new Employee()
        {
            Id = new Guid(),
            Name = employeeDto.Name,
            LastName = employeeDto.LastName,
            Age = employeeDto.Age,
            Gmail = employeeDto.Gmail,
            TypeEmployee = employeeDto.TypeEmployee,
            File = employees.Count + 1,
            Domicile = employeeDto.Domicile,
            User = new User
            {
                UserName = employeeDto.UserName,
                Password = CodePassword(employeeDto.Password)
            }

        };
        await _repository.Agregar(employee);
        if (employeeDto == null) return null;
        return employeeDto;
    }
    
    public async Task<EmployeeDto?> DeleteEmployee(EmployeeDto? employeeDto)
    {
        if (employeeDto == null) return null;
        var employeeFound = await _repository.ObtenerElPrimero<Employee>(e => e.Gmail == employeeDto.Gmail);
        if (employeeFound == null) return null;
        await _repository.Eliminar(employeeFound);
        return employeeDto;
    }

    public async Task<EmployeeDto?> UpdateEmployee(EmployeeDto employeeDto)
    {
        if (employeeDto == null) return null;
        var employeeFound = await _repository.ObtenerElPrimero<Employee>(e => e.Gmail == employeeDto.Gmail);
        if (employeeFound == null) return null;

        employeeFound.Name = employeeDto.Name;
        employeeFound.LastName = employeeDto.LastName;
        employeeFound.Age = employeeDto.Age;
        employeeFound.TypeEmployee = employeeDto.TypeEmployee;
        employeeFound.Domicile = employeeDto.Domicile;

        await _repository.Actualizar(employeeFound);
        return employeeDto;
    }
    private string CodePassword(string? passwordInput) => BCrypt.Net.BCrypt.HashPassword(passwordInput);
}




