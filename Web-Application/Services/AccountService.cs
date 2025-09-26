using Web_Application.DTOs;
using Web_Application.Exceptions;
using Web_Application.Interfaces;
using Web_Domain.Entities;
using Web_Domain.Entities.Rule;
using Web_Domain.Repository;

namespace Web_Application.Services;

public class AccountService : IAccountService
{
    private readonly IRepository _repository;
    public AccountService(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<AccountDto.PayResponse?> PayFee(string idEmployeeA, Guid idInscription)
    {
        var employeeFound = await _repository.ObtenerPorId<Employee>(Guid.Parse(idEmployeeA));
        if (employeeFound == null) throw new EntityNotFoundException("El empleado no se encontró.");
        var inscriptionFound = await _repository.ObtenerPorId<Inscription>(idInscription, nameof(Inscription.Pays), nameof(Client));
        if (inscriptionFound == null) throw new EntityNotFoundException("La inscripción no se encontró.");
        var lastPay = inscriptionFound.Pays!.LastOrDefault();
        var lastFee = await _repository.ObtenerPorId<Fee>(lastPay!.FeeId);
        if (!lastFee!.IsCancelled) throw new BusinessConflictException("La cuota del cliente no fue cancelada, no se puede realizar otro pago hasta que se cancele");
        if(lastFee!.IsActive) throw new BusinessConflictException("El cliente esta activo, no se puede realizar otro pago hasta que este se termine.");
        var rules = await _repository.ListarTodos<Rule>();
        var payFee = new Pay
        {
            Id = Guid.NewGuid(),
            PayDate = DateTime.Now,
            Fee = new Fee
            {
                Id = Guid.NewGuid(),
                FeeNumber = inscriptionFound.Pays!.Count + 1,
                Value = rules.FirstOrDefault()!.Value,
                InitialDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
            },
            EmployeeId = employeeFound.Id,
            InscriptionId = inscriptionFound.Id
        };

        await _repository.Agregar(payFee);
        return new AccountDto.PayResponse(payFee.PayDate, payFee.Fee.EndDate, payFee.Fee.Value, true);
    }
    public async Task<AccountDto.CancelResponse?> CancelFee(string idEmployeeA, Guid idInscription)
    {
        var employeeFound = await _repository.ObtenerPorId<Employee>(Guid.Parse(idEmployeeA));
        if(employeeFound == null) throw new EntityNotFoundException("El empleado no se encontró.");
        var inscriptionFound = await _repository.ObtenerPorId<Inscription>(idInscription, nameof(Inscription.Pays), nameof(Client));
        if(inscriptionFound == null) throw new EntityNotFoundException("La inscripción no se encontró.");
        var lastPay = inscriptionFound.Pays!.LastOrDefault();
        var feeCancel = await _repository.ObtenerPorId<Fee>(lastPay!.FeeId);
        if (feeCancel == null || !feeCancel.IsActive) throw new BusinessConflictException("No hay un pago activo para cancelar.");
        feeCancel.IsCancelled = true;
        await _repository.Actualizar(feeCancel);
        return new AccountDto.CancelResponse(inscriptionFound.Client!.Name!, inscriptionFound.Client!.LastName! , feeCancel.FeeNumber, true);
    }
}
