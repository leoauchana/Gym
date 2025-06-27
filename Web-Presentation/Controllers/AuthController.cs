using Microsoft.AspNetCore.Mvc;
using Web_Application.DTOs;
using Web_Application.Interfaces;
using Web_Domain.Entities;

namespace Web_Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost]
    public async Task<IActionResult> Login(UserDto user)
    {
        // TODO: Terminar de ver la libreria NLog para podes registrar los inicios de sesión
        var userValid = await _authService.LoginEmployee(user);
        if(userValid.Item1 == null || userValid.Item2 == null) return NotFound("El nombre de usuario o contraseña son incorrectos");
        return Ok(new
        {
            Success = true,
            Empleado = userValid.Item1,
            Token = userValid.Item2,
        });
    }
    //[HttpGet]
    //public async Task<IActionResult> Logout()
    //{
    //    await _authService.LogoutEmployee();
    //    return null;
    //}
}
