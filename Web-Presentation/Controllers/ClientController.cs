using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

    [HttpGet]
    public async Task<IActionResult> Register(ClientDto? idClient)
    {
        var idEmployee = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var clientRegister = await _clientService.RegisterClient(idEmployee, idClient);
        if (clientRegister == null) return BadRequest("Error al registrar el cliente");
        return Ok("Se registro el cliente correctamente");
    }

}
