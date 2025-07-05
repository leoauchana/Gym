using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web_Application.DTOs;
using Web_Application.Interfaces;

namespace Web_Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PayController : ControllerBase
{
    private readonly IPayService _payService;

    public PayController(IPayService payService)
    {
        _payService = payService;
    }

    [HttpPost]
    public async Task<IActionResult> PayFee(Guid idClient)
    {
        var idEmployee = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var clientPay = await _payService.PayFee(idEmployee, idClient);
        return Ok();
    }
}
