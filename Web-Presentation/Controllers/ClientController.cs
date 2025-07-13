using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web_Application.DTOs;
using Web_Application.Interfaces;

namespace Web_Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }
    [Authorize(Policy = "Receptionist")]
    [HttpPost]
    public async Task<IActionResult> Register(ClientDto.Request? clientDto)
    {
        var idEmployee = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idEmployee == null) return BadRequest("No se pudo obtener el ID del empleado");
        var clientRegister = await _clientService.RegisterClient(idEmployee, clientDto);
        if (clientRegister == null) return BadRequest("Error al registrar el cliente");
        return Ok(new
        {
            Message = "Cliente registrado con éxito",
            clientRegister
        });
    }

}
