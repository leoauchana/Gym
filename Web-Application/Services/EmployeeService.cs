using Microsoft.AspNetCore.Rewrite;
using Web_Application.DTOs;
using Web_Application.Exceptions;
using Web_Application.Interfaces;
using Web_Domain.Entities;
using Web_Domain.Entities.Rule;
using Web_Domain.Logs;
using Web_Domain.Repository;
using static Web_Application.DTOs.RuleDto;

namespace Web_Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository _repository;
    public EmployeeService(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<EmployeeDto.EmployeeResponse?> RegisterEmployee(string idEmployee, EmployeeDto.EmployeeRequest employeeDto)
    {
        var registerBy = await _repository.ObtenerPorId<Employee>(Guid.Parse(idEmployee));
        if(registerBy == null) throw new EntityNotFoundException($"El empleado autenticado no se encontro.{idEmployee}");
        var employees = await _repository.ListarTodos<Employee>();
        var employeeFound = employees.Where(e => e.Email == employeeDto!.gmail).FirstOrDefault();
        if (employeeFound != null) throw new BusinessConflictException("El empleado ya se encuentra registrado");
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
        await RegisterEmployeeLog(newEmployee.Id, registerBy.Id, employeeDto.description);
        return new EmployeeDto.EmployeeResponse
        (
            newEmployee.Name,
            newEmployee.LastName,
            newEmployee.Email,
            newEmployee.File,
            newEmployee.TypeEmployee.ToString()
        );
    }
    public async Task<EmployeeDto.EmployeeResponse?> DeleteEmployee(string idEmployeeA, Guid idEmployeeDelete)
    {
        var employeeFound = await _repository.ObtenerElPrimero<Employee>(e => e.Id == idEmployeeDelete);
        if (employeeFound == null) throw new EntityNotFoundException("El empleado no se encontro");
        await _repository.Eliminar(employeeFound);
        return new EmployeeDto.EmployeeResponse(employeeFound.Name, employeeFound.LastName, employeeFound.Email, employeeFound.File, employeeFound.TypeEmployee.ToString());
    }
    public async Task<EmployeeDto.EmployeeResponse?> UpdateEmployee(string idEmployeeA, EmployeeDto.EmployeeRequest employeeDto)
    {
        var employeeFound = await _repository.ObtenerElPrimero<Employee>(e => e.Email == employeeDto.gmail);
        if (employeeFound == null) throw new EntityNotFoundException("El empleado no se encontro.");

        employeeFound.Name = employeeDto.name;
        employeeFound.LastName = employeeDto.lastName;
        employeeFound.Age = employeeDto.age;
        employeeFound.TypeEmployee = employeeDto.typeEmployee;
        employeeFound.Domicile = employeeDto.domicile;

        await _repository.Actualizar(employeeFound);
        return new EmployeeDto.EmployeeResponse
        (
            employeeFound.Name,
            employeeFound.LastName,
            employeeFound.Email,
            employeeFound.File,
            employeeFound.TypeEmployee.ToString()
        );
    }
    public async Task<List<EmployeeDto.EmployeeResponse>> GetAllEmployees()
    {
        var employees = await _repository.ListarTodos<Employee>();
        if(employees == null || !(employees!.Count > 0)) throw new EntityNotFoundException("No se encontraron empleados registrados.");
        return employees.Select(e => new EmployeeDto.EmployeeResponse
        (
            e.Name,
            e.LastName,
            e.Email,
            e.File,
            e.TypeEmployee.ToString()
        )).ToList();
    }
    public async Task<EmployeeDto.EmployeeResponse?> GetEmployeeById(Guid idEmployee)
    {
        var employeeFound = await _repository.ObtenerPorId<Employee>(idEmployee);
        if(employeeFound == null) throw new EntityNotFoundException("El empleado no se encontro.");
        return new EmployeeDto.EmployeeResponse
        (
            employeeFound.Name,
            employeeFound.LastName,
            employeeFound.Email,
            employeeFound.File,
            employeeFound.TypeEmployee.ToString()
        );
    }
    public async Task<bool> SetValueRule(string idEmployeeA, RuleRequest rule)
    {
        if (!Guid.TryParse(idEmployeeA, out var idEmployee)) return false;
        var employeeFound = await _repository.ObtenerPorId<Employee>(idEmployee);
        if(employeeFound == null) throw new EntityNotFoundException("El empleado autenticado no se encontro.");
        var newRule = new Rule
        {
            Id = Guid.NewGuid(),
            Value = rule.valueRule,
            UpdatedDate = DateTime.Now
        };
        await _repository.Agregar(newRule);
        return true;
    }
    private async Task RegisterEmployeeLog(Guid employeeRegisteredId, Guid registeredById, string description)
    {
        var newRegisterEmployee = new LogEmployeeRegister
        {
            Id = Guid.NewGuid(),
            Description = description,
            RegisterDate = DateTime.Now,
            NewEmployeeId = employeeRegisteredId,
            RegisterById = registeredById
        };
        await _repository.Agregar(newRegisterEmployee);
    }
    private string CodePassword(string? passwordInput) => BCrypt.Net.BCrypt.HashPassword(passwordInput);
}