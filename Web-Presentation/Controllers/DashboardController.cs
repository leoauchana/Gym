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
        try
        {
            var access = await _dashboarService.GetAccess();
            if (access == null) return NotFound(new
            {
                Message = "No hay registros de acceso"
            });
            return Ok(new
            {
                Message = "Clientes registrados",
                access
            });
        }
        catch(EntityNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [Authorize(Policy = "Administrator")]
    [HttpGet("clientsRegistered")]
    public async Task<IActionResult> ClientsRegistered()
    {
        try
        {
            var clientsRegisteredWithEmployee = await _dashboarService.GetAccess();
            return Ok(new
            {
                clientsRegisteredWithEmployee
            });
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch(NullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
