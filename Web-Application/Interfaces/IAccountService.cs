using Web_Application.DTOs;

namespace Web_Application.Interfaces;

public interface IAccountService
{
    public Task<AccountDto.PayResponse?> PayFee(string idEmployee, Guid idInscription);
    public Task<AccountDto.CancelResponse?> CancelFee(string idEmployee, Guid idInscription);
}
