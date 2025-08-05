using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web_Application.DTOs;
using Web_Application.Exceptions;
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

    [Authorize(Policy = "AdminAndReceptionist")]
    [HttpPost("{idClient}")]
    public async Task<IActionResult> PayFee(Guid idClient)
    {
            var idEmployee = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (idEmployee == null) return BadRequest("No se pudo obtener el ID del empleado");
            var newPay = await _payService.PayFee(idEmployee, idClient);
            if(newPay == null || newPay.isSuccess) return BadRequest("Error al realizar el pago");
                return Ok(new
                {
                    Mesaage = "Pago realizado con exito",
                    newPay
                });
    }
}
