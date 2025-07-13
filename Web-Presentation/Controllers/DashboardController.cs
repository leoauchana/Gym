using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    [Authorize(Policy = "Admin")]
    [HttpGet]
    public async Task<IActionResult> AccessRegistered()
    {
        var access = await _dashboarService.GetAccess();
        if(access == null) return NotFound(new 
        {
            Message = "No hay registros de acceso"
        });
        return Ok(new 
        {
            Message = "Clientes registrados",
            access
        });
    }
}
