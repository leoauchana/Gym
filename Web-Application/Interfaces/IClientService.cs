using Web_Application.DTOs;

namespace Web_Application.Interfaces;

public interface IClientService
{
    public Task<ClientDto?> RegisterClient(string idEmployee, ClientDto clientDto);
    public Task<ClientDto?> DeleteClient(ClientDto? clientDto);
}
