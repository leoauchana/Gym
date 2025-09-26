using Web_Application.DTOs;

namespace Web_Application.Interfaces;

public interface IClientService
{
    public Task<ClientDto.RegisterResponse?> RegisterClient(string idEmployee, ClientDto.ClientRequest clientDto);
    public Task<List<ClientDto.ClientResponse>?> GetAllActivesClients();
    public Task<List<ClientDto.ClientResponse>?> GetAllClients();
    public Task<ClientDto.ClientResponse?> GetById(Guid idClient);
    public Task<ClientDto.ClientResponse?> DeleteClient(Guid idClient);
}
