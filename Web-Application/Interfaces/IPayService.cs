using Web_Application.DTOs;

namespace Web_Application.Interfaces;

public interface IPayService
{
    public Task<ClientDto> PayFee(string idEmployee, Guid idClient);
}
