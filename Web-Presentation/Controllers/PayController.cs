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
    [HttpPost]
    public async Task<IActionResult> PayFee([FromBody] PayDto.PayRequest payDto)
    {
        try
        {
            var idEmployee = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (idEmployee == null) return BadRequest("No se pudo obtener el ID del empleado");
            var newPay = await _payService.PayFee(idEmployee, payDto);
            if(newPay == null || newPay.isSuccess) return BadRequest("Error al realizar el pago");
                return Ok(new
                {
                    Mesaage = "Pago realizado con exito",
                    newPay.payDate,
                    newPay.value,
                    newPay.isSuccess
                });
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
