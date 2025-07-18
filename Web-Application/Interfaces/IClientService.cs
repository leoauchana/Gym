using Web_Application.DTOs;

namespace Web_Application.Interfaces;

public interface IClientService
{
    public Task<ClientDto.ClientResponse?> RegisterClient(string idEmployee, ClientDto.ClientRequest clientDto);
    public Task<ClientDto.ClientResponse?> DeleteClient(Guid clientDto);
}
