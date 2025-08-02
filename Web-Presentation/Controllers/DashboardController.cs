using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Application.Exceptions;
using Web_Application.Interfaces;

namespace Web_Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    public readonly IDashboarService _dashboarService;
    public DashboardController(IDashboarService dashboarService)
    {
        _dashboarService = dashboarService;
    }

    [Authorize(Policy = "Administrator")]
    [HttpGet("accessRegistered")]
    public async Task<IActionResult> AccessRegistered()
    {
            var access = await _dashboarService.GetAccess();
            if (access == null) return BadRequest("Hubo un error al obtener los accesos.");
            return Ok(new
            {
                Message = "Clientes registrados",
                access
            });
    }
    [Authorize(Policy = "Administrator")]
    [HttpGet("clientsRegistered")]
    public async Task<IActionResult> ClientsRegistered()
    {
            var clientsRegisteredWithEmployee = await _dashboarService.GetAccess();
            if (clientsRegisteredWithEmployee == null) return BadRequest("Hubo un error al obtener los clientes registrados");
            return Ok(new
            {
                clientsRegisteredWithEmployee
            });
    }
    [Authorize(Policy = "Administrator")]
    [HttpGet("employeesRegistered")]
    public async Task<IActionResult> EmployeesRegistered()
    {
            var employeesRegistered = await _dashboarService.GetEmployeesRegistered();
            if (employeesRegistered == null) return BadRequest("Hubo un error al obtener a los empleados registrados.");
            return Ok(employeesRegistered);
    }
}

