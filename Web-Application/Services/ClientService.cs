using Web_Application.DTOs;
using Web_Application.Interfaces;
using Web_Domain.Entities;
using Web_Domain.Repository;

namespace Web_Application.Services;

public class ClientService : IClientService
{
    private readonly IRepository _repository;
    public ClientService(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<ClientDto?> RegisterClient(string idEmployeeS, ClientDto? clientDto)
    {
        if(!Guid.TryParse(idEmployeeS, out var idEmployee)) return null;
        var clientFound = await _repository.ObtenerElPrimero<Client>(c => c.Dni == clientDto!.Dni);
        if (clientFound == null) return null;
        var employeeFound = await _repository.ObtenerElPrimero<Employee>(e => e.Id == idEmployee);
        var inscriptionsRegistered = await _repository.ObtenerTodos<Inscription>();
        var newInscription = new Inscription()
        {
            Id = new Guid(),
            InscriptionNumber = inscriptionsRegistered.Count + 1,
            Client = new Client()
               {
               Id = new Guid(),
               Name = clientDto!.Name,
               LastName = clientDto.LastName,
               Age = clientDto.Age,
               Dni = clientDto.Dni,
               Domicile = clientDto.Domicile,
            },
            Employee = employeeFound,
            InscriptionDate = DateTime.Now
        };
        await _repository.Agregar(newInscription);
        return clientDto;
    }

    public async Task<ClientDto?> DeleteClient(ClientDto? clientDto)
    {
        var clientFound = await _repository.ObtenerElPrimero<Client>(c => c.Dni == clientDto.Dni);
        if (clientFound == null) return null;
        await _repository.Eliminar(clientFound);
        return clientDto;
    }
}
