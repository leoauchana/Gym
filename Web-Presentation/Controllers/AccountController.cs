using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web_Application.Interfaces;

namespace Web_Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PayController : ControllerBase
{
    private readonly IAccountService _accountService;

    public PayController(IAccountService payService)
    {
        _accountService = payService;
    }

    [Authorize(Policy = "AdminAndReceptionist")]
    [HttpPost("{idInscription}")]
    public async Task<IActionResult> PayFee(Guid idInscription)
    {
            var idEmployee = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (idEmployee == null) return BadRequest("No se pudo obtener el ID del empleado");
            var newPay = await _accountService.PayFee(idEmployee, idInscription);
            if(newPay == null || !newPay.isSuccess) return BadRequest("Error al realizar el pago");
                return Ok(new
                {
                    Mesaage = "Pago realizado con exito",
                    newPay
                });
    }
    [Authorize(Policy = "Administrator")]
    [HttpPatch("{idInscription}")]
    public async Task<IActionResult> CancelFee(Guid idInscription)
    {
        var idEmployee = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idEmployee == null) return BadRequest("No se pudo obtener el ID del empleado");
        var cancelFee = await _accountService.CancelFee(idEmployee, idInscription);
        if(cancelFee == null) return BadRequest("Error al cancelar el pago");
        return Ok(new
        {
            Message = "Pago cancelado con éxito",
            cancelFee
        });
    }
}
