using Web_Application.DTOs;

namespace Web_Application.Interfaces;

public interface IPayService
{
    public Task<PayDto.PayResponse?> PayFee(string idEmployee, PayDto.PayRequest payDto);
}
