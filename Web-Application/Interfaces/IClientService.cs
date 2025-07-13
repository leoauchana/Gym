using Web_Application.DTOs;

namespace Web_Application.Interfaces;

public interface IClientService
{
    public Task<ClientDto.Response?> RegisterClient(string idEmployee, ClientDto.Request clientDto);
    public Task<ClientDto.Response?> DeleteClient(Guid clientDto);
}
