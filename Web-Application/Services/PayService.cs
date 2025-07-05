using Web_Application.DTOs;
using Web_Application.Interfaces;
using Web_Domain.Entities;
using Web_Domain.Repository;

namespace Web_Application.Services;

public class PayService : IPayService
{
    private readonly IRepository _repository;
    public PayService(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<ClientDto?> PayFee(string? idEmployeeA, Guid idClient)
    {
        if (!Guid.TryParse(idEmployeeA, out var idEmployee)) return null;
        var employeeFound = await _repository.ObtenerPorId<Employee>(idEmployee);
        if (employeeFound == null) return null;
        var clientFound = await _repository.ObtenerPorId<Client>(idClient, nameof(Inscription));
        if (clientFound == null || clientFound.Inscription == null) return null;
        var inscriptionClient = clientFound.Inscription;
        var payFee = new Pay
        {
            Fee = new Fee
            {
                FeeNumber = inscriptionClient.Pays!.Count + 1,
            }
        };
        return null;
    }
}
