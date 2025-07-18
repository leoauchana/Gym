using Web_Application.DTOs;
using Web_Application.Exceptions;
using Web_Application.Interfaces;
using Web_Domain.Entities;
using Web_Domain.Repository;
using Web_Domain.Rules;

namespace Web_Application.Services;

public class PayService : IPayService
{
    private readonly IRepository _repository;
    private readonly IValueRule _valueRule;
    public PayService(IRepository repository, IValueRule valueRule)
    {
        _repository = repository;
        _valueRule = valueRule;
    }
    public async Task<PayDto.PayResponse?> PayFee(string idEmployeeA, PayDto.PayRequest payDto)
    {
        if (!Guid.TryParse(idEmployeeA, out var idEmployee)) return null;
        var employeeFound = await _repository.ObtenerPorId<Employee>(idEmployee);
        var clientFound = await _repository.ObtenerPorId<Client>(payDto.idClient, nameof(Inscription));
        var inscriptionClient = clientFound?.Inscription;
        if (employeeFound == null
            || clientFound == null
            || inscriptionClient == null
            || inscriptionClient.Pays == null)
            throw new EntityNotFoundException("El empleado autenticado o cliente no se encontró");
        if(inscriptionClient == null
            || inscriptionClient.Pays == null) throw new EntityNotFoundException("La inscripción del cliente no se encontró o no tiene pagos registrados.");
        var payFee = new Pay
        {
            Id = Guid.NewGuid(),
            PayDate = DateTime.Now,
            Fee = new Fee
            {
                Id = Guid.NewGuid(),
                FeeNumber = inscriptionClient.Pays!.Count + 1,
                Value = _valueRule.GetValue()
            },
            EmployeeId = employeeFound.Id,
            InscriptionId = inscriptionClient.Id
        };

        await _repository.Agregar(payFee);
        return new PayDto.PayResponse(payFee.PayDate, payFee.Fee.Value, true);
    }
}
