using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web_Application.DTOs;
using Web_Application.Exceptions;
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
    [Authorize(Policy = "AdminAndReceptionist")]
    [HttpPost("registerClient")]
    public async Task<IActionResult> Register(ClientDto.ClientRequest? clientDto)
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
    [Authorize(Policy = "Administrator")]
    [HttpGet("getAllActives")]
    public async Task<IActionResult> GetAllActives()
    {
        var clientsActives = await _clientService.GetAllActivesClients();
        if (clientsActives == null) return BadRequest("Hubo un error al obtener los clientes activos.");
        return Ok(new 
        {
            Message= "Clientes activos.",
            clientsActives
        }
        );
    }
    [Authorize( Policy = "AdminAndReceptionist")]
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var clientsRegistered = await _clientService.GetAllClients();
        if(clientsRegistered == null) return BadRequest("Hubo un error al obtener los clientes registrados en el sistema.");
        return Ok(new
        {
            Message = "Clientes registrados.",
            clientsRegistered
        }
        );
    }
    [Authorize(Policy ="Administrator")]
    [HttpPost("deleteClient")]
    public async Task<IActionResult> Delete(Guid idClient)
    {
            var clientDeleted = await _clientService.DeleteClient(idClient);
            if (clientDeleted == null) return BadRequest("Error al eliminar el cliente.");
            return Ok(new
            {
                Message = "Cliente eliminado con éxito.",
                clientDeleted
            });
    }
    
}
