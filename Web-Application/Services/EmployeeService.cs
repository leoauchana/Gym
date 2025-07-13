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
    public async Task<EmployeeDto.Response?> RegisterEmployee(string idEmployee, EmployeeDto.Request employeeDto)
    {
        var employees = await _repository.ListarTodos<Employee>();
        var employeeFound = employees.Where(e => e.Email == employeeDto!.gmail).FirstOrDefault();
        if (employeeFound == null) return null;
        var newEmployee = new Employee()
        {
            Id = Guid.NewGuid(),
            Name = employeeDto!.name,
            LastName = employeeDto.lastName,
            Age = employeeDto.age,
            Email = employeeDto.gmail,
            TypeEmployee = employeeDto.typeEmployee,
            File = employees.Count + 1,
            Domicile = employeeDto.domicile,
            User = new User
            {
                Id = Guid.NewGuid(),
                UserName = employeeDto.userName,
                Password = CodePassword(employeeDto.password)
            }

        };
        await _repository.Agregar(newEmployee);
        if (employeeDto == null) return null;
        return new EmployeeDto.Response
        (
            newEmployee.Name,
            newEmployee.LastName,
            newEmployee.Email,
            newEmployee.File,
            newEmployee.TypeEmployee.ToString()
        );
    }
    public async Task<EmployeeDto.Response?> DeleteEmployee(string idEmployeeA, Guid idEmployeeDelete)
    {
        var employeeFound = await _repository.ObtenerElPrimero<Employee>(e => e.Id == idEmployeeDelete);
        if (employeeFound == null) return null;
        await _repository.Eliminar(employeeFound);
        return new EmployeeDto.Response(employeeFound.Name, employeeFound.LastName, employeeFound.Email, employeeFound.File, employeeFound.TypeEmployee.ToString());
    }
    public async Task<EmployeeDto.Response?> UpdateEmployee(string idEmployeeA, EmployeeDto.Request employeeDto)
    {
        var employeeFound = await _repository.ObtenerElPrimero<Employee>(e => e.Email == employeeDto.gmail);
        if (employeeFound == null) return null;

        employeeFound.Name = employeeDto.name;
        employeeFound.LastName = employeeDto.lastName;
        employeeFound.Age = employeeDto.age;
        employeeFound.TypeEmployee = employeeDto.typeEmployee;
        employeeFound.Domicile = employeeDto.domicile;

        await _repository.Actualizar(employeeFound);
        return new EmployeeDto.Response
        (
            employeeFound.Name,
            employeeFound.LastName,
            employeeFound.Email,
            employeeFound.File,
            employeeFound.TypeEmployee.ToString()
        );
    }
    public async Task<bool> SetValueRule(string idEmployeeA, double valueRule)
    {
        if (!Guid.TryParse(idEmployeeA, out var idEmployee)) return false;
        var employeeFound = await _repository.ObtenerPorId<Employee>(idEmployee);
        return true;
    }
    private string CodePassword(string? passwordInput) => BCrypt.Net.BCrypt.HashPassword(passwordInput);
}