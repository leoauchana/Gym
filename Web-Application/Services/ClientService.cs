using Web_Application.DTOs;
using Web_Application.Exceptions;
using Web_Application.Interfaces;
using Web_Domain.Entities;
using Web_Domain.Entities.Rule;
using Web_Domain.Logs;
using Web_Domain.Repository;

namespace Web_Application.Services;

public class ClientService : IClientService
{
    private readonly IRepository _repository;
    public ClientService(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<ClientDto.RegisterResponse?> RegisterClient(string idEmployeeS, ClientDto.ClientRequest? clientDto)
    {
        if(!Guid.TryParse(idEmployeeS, out var idEmployee)) return null;
        var employeeFound = await _repository.ObtenerElPrimero<Employee>(e => e.Id == idEmployee);
        if (employeeFound == null) throw new EntityNotFoundException("El empleado autenticado no se encontro.");
        var clientFound = await _repository.ObtenerElPrimero<Client>(c => c.Dni == clientDto!.dni);
        if (clientFound != null) throw new EntityNotFoundException("El cliente ya esta registrado.");
        var inscriptionsRegistered = await _repository.ObtenerTodos<Inscription>();
        var rules = await _repository.ListarTodos<Rule>();
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
                Value = rules.FirstOrDefault()!.Value,
                InitialDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
            },
            EmployeeId = employeeFound!.Id,
            InscriptionId = newInscription.Id
        };
        await _repository.Agregar(newInscription);
        await _repository.Agregar(newPay);
        await RegisterClientLog(newInscription.Client, employeeFound);
        return new ClientDto.RegisterResponse
        (
            clientDto.name,
            clientDto.lastName,
            clientDto.dni,
            clientDto.domicile,
            clientDto.age,
            newPay.Fee.InitialDate,
            newPay.Fee.EndDate
        );
    }
    public async Task<ClientDto.ClientResponse?> DeleteClient(Guid idClientDelete)
    {
        var clientFound = await _repository.ObtenerElPrimero<Client>(c => c.Id == idClientDelete);
        if (clientFound == null) throw new EntityNotFoundException("El cliente no se encontro.");
        await _repository.Eliminar(clientFound);
        return new ClientDto.ClientResponse
        (
            clientFound.Name,
            clientFound.LastName,
            clientFound.Dni,
            clientFound.Domicile,
            clientFound.Age
        );
    }
    public async Task<List<ClientDto.ClientResponse>?> GetAllActivesClients()
    {
        var clients = await _repository.ListarTodosCon<Fee>(
            f => f.Pay!,
            f => f.Pay!.Inscription!,
            f => f.Pay!.Inscription!.Client!);
        if (clients == null || clients.Count <= 0) throw new NullException("No hay clientes activos");
        var clientsActives = clients.Where(f => f.IsActive && !f.IsCancelled).Select(f => new ClientDto.ClientResponse(f.Pay!.Inscription!.Client!.Name,
            f.Pay.Inscription.Client!.LastName,
            f.Pay.Inscription.Client!.Dni,
            f.Pay.Inscription.Client!.Domicile,
            f.Pay.Inscription.Client!.Age)).ToList();
        return clientsActives;
    }
    public async Task<List<ClientDto.ClientResponse>?> GetAllClients()
    {
        var clientsRegistered = await _repository.ListarTodos<Client>();
        if (clientsRegistered == null) throw new EntityNotFoundException("No existe un registro de clientes.");
        if (!(clientsRegistered.Count > 0)) throw new NullException("No se encuentran clientes registrados.");
        var clientesRegisteredRes = clientsRegistered.Select(c => new ClientDto.ClientResponse
        (
            c.Name,
            c.LastName,
            c.Dni,
            c.Domicile,
            c.Age
        )).ToList();
        return clientesRegisteredRes;
    }
    private async Task RegisterClientLog(Client client, Employee employee)
    {
        var newLog = new LogClientsRegister
        {
            Id = Guid.NewGuid(),
            Client = client,
            Employee = employee,
            RegisterDate = DateTime.Now
        };
        await _repository.Agregar(newLog);
    }
    public async Task<ClientDto.ClientResponse?> GetById(Guid idClient)
    {
        var clientRegistered = await _repository.ObtenerPorId<Client>(idClient);
        if(clientRegistered == null) throw new EntityNotFoundException("El cliente no se encontro.");
        return new ClientDto.ClientResponse
        (
            clientRegistered.Name,
            clientRegistered.LastName,
            clientRegistered.Dni,
            clientRegistered.Domicile,
            clientRegistered.Age
        );
    }
}