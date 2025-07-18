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
        var access = await _repository.ListarTodos<LogAccess>(nameof(User), nameof(User.Employee));
        if (access == null) throw new EntityNotFoundException("No se encontro un log de acceso");
        if (!(access.Count > 0)) throw new NullException("Esta vacio el log de acceso");
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
        if(!(clientsRegistered.Count > 0)) throw new NullException("Esta vacio el log de clientes registrados");
        var clientsWithEmployee = clientsRegistered.Select(r => new ClientsRegisterDto.ClientsRegisterResponse(
            new ClientDto.ClientResponse(r.Client!.Name, r.Client.LastName, r.Client.Dni, r.Client.Domicile, r.Client.Age),
            new EmployeeDto.EmployeeResponse(r.Employee!.Name, r.Employee.LastName, r.Employee.Email, r.Employee.File, r.Employee.TypeEmployee.ToString()),
            r.RegisterDate
            )).ToList();
        return clientsWithEmployee;
    }
}
