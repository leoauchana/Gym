using Web_Application.DTOs;
using Web_Application.DTOs.Dashboard;
using Web_Application.Exceptions;
using Web_Application.Interfaces;
using Web_Domain.Entities;
using Web_Domain.Logs;
using Web_Domain.Repository;

namespace Web_Application.Services;

public class DashboardService : IDashboarService
{
    private readonly IRepository _repository;
    public DashboardService(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<AccessDto.AccessResponse>?> GetAccess()
    {
        var access = await _repository.ListarTodosCon<LogAccess>(
            l => l.User!,
            l => l.User!.Employee!
            );
        if (access == null) throw new EntityNotFoundException("No se encontro un log de acceso");
        var accessDto = access.Select(a => new AccessDto.AccessResponse
        (
            a.isSuccess,
            a.AccessDate,
            new UserDto.UserResponseLog(a.User!.Employee!.Name, a.User!.Employee!.LastName, a.User!.Employee.Email, a.User.Employee.File, a.User!.Employee!.TypeEmployee.ToString())
        )).ToList();
        return accessDto;
    }
    public async Task<List<ClientsRegisterDto.ClientsRegisterResponse>?> GetClientsRegistered()
    {
        var clientsRegistered = await _repository.ListarTodos<LogClientsRegister>(nameof(Client), nameof(Employee));
        if(clientsRegistered == null) throw new EntityNotFoundException("No se encontro un log de clientes registrados");
        if(!(clientsRegistered.Count > 0)) throw new NullException("No hay clientes registrados");
        var clientsWithEmployee = clientsRegistered.Select(r => new ClientsRegisterDto.ClientsRegisterResponse(
            new ClientDto.ClientResponse(r.Client!.Name, r.Client.LastName, r.Client.Dni, r.Client.Domicile, r.Client.Age),
            new EmployeeDto.EmployeeResponse(r.Employee!.Name, r.Employee.LastName, r.Employee.Email, r.Employee.File, r.Employee.TypeEmployee.ToString()),
            r.RegisterDate
            )).ToList();
        return clientsWithEmployee;
    }
    public async Task<List<EmployeeRegisterDto.EmployeeRegisterResponse>?> GetEmployeesRegistered()
    {
        var employeesRegistered = await _repository.ListarTodosCon<LogEmployeeRegister>(
            l => l.RegisterBy!,
            l => l.NewEmployee!
            );
        if (employeesRegistered == null) throw new EntityNotFoundException("No se encontro un log de empleados registrados");
        var employeesWithRegisterBy = employeesRegistered.Select(e => new EmployeeRegisterDto.EmployeeRegisterResponse(
            e.RegisterDate,
            new EmployeeDto.EmployeeResponse(e.NewEmployee!.Name, e.NewEmployee.LastName, e.NewEmployee.Email, e.NewEmployee.File, e.NewEmployee.TypeEmployee.ToString()),
            new EmployeeDto.EmployeeResponse(e.RegisterBy!.Name, e.RegisterBy.LastName, e.RegisterBy.Email, e.RegisterBy.File, e.RegisterBy.TypeEmployee.ToString()),
            e.Description!
            )).ToList();
        return employeesWithRegisterBy;
    }
}
