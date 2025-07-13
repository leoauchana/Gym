using Web_Application.DTOs;
using Web_Application.Interfaces;
using Web_Domain.Entities;
using Web_Domain.Repository;
using Web_Domain.Rules;

namespace Web_Application.Services;

public class ClientService : IClientService
{
    private readonly IRepository _repository;
    private readonly IValueRule _valueRule;
    public ClientService(IRepository repository, IValueRule valueRule)
    {
        _repository = repository;
        _valueRule = valueRule;
    }
    public async Task<ClientDto.Response?> RegisterClient(string idEmployeeS, ClientDto.Request? clientDto)
    {
        if(!Guid.TryParse(idEmployeeS, out var idEmployee)) return null;
        var clientFound = await _repository.ObtenerElPrimero<Client>(c => c.Dni == clientDto!.dni);
        if (clientFound == null) return null;
        var employeeFound = await _repository.ObtenerElPrimero<Employee>(e => e.Id == idEmployee);
        if (employeeFound == null) return null;
        var inscriptionsRegistered = await _repository.ObtenerTodos<Inscription>();
        var newInscription = new Inscription()
        {
            Id = Guid.NewGuid(),
            InscriptionNumber = inscriptionsRegistered.Count + 1,
            Client = new Client()
            {
               Id = new Guid(),
               Name = clientDto!.name,
               LastName = clientDto.lastName,
               Age = clientDto.age,
               Dni = clientDto.dni,
               Domicile = clientDto.domicile,
            },
            InscriptionDate = DateTime.Now
        };
        var newPay = new Pay
        {
            Id = Guid.NewGuid(),
            PayDate = DateTime.Now,
            Fee = new Fee
            {
                FeeNumber = 1,
                Id = Guid.NewGuid(),
                Value = _valueRule.GetValue()
            },
            EmployeeId = employeeFound!.Id,
            InscriptionId = newInscription.Id
        };
        await _repository.Agregar(newInscription);
        await _repository.Agregar(newPay);
        return new ClientDto.Response
        (
            clientDto.name,
            clientDto.lastName,
            clientDto.dni,
            clientDto.domicile,
            clientDto.age
        );
    }
    public async Task<ClientDto.Response?> DeleteClient(Guid idClientDelete)
    {
        var clientFound = await _repository.ObtenerElPrimero<Client>(c => c.Id == idClientDelete);
        if (clientFound == null) return null;
        await _repository.Eliminar(clientFound);
        return new ClientDto.Response
        (
            clientFound.Name,
            clientFound.LastName,
            clientFound.Dni,
            clientFound.Domicile,
            clientFound.Age
        );
    }