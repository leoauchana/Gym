using Web_Application.DTOs;

namespace Web_Application.Interfaces;

public interface IPayService
{
    public Task<PayDto.Response?> PayFee(string idEmployee, PayDto.Request payDto);
}
