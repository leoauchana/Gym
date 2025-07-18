using Web_Application.DTOs;
using Web_Application.DTOs.Dashboard;

namespace Web_Application.Interfaces;

public interface IDashboarService
{
    Task<List<AccessDto.AccessResponse>?> GetAccess();
    Task<List<ClientsRegisterDto.ClientsRegisterResponse>?> GetClientsRegistered();
}
