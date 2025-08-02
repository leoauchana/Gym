using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Application.DTOs;
using Web_Application.Interfaces;

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
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserDto.UserRequest user)
    {
            var userValid = await _authService.LoginEmployee(user);
            return Ok(new
            {
                Success = true,
                Nombre = userValid!.name,
                Apellido = userValid.lastName,
                Gmail = userValid.gmail,
                File = userValid.file,
                TypeEmployee = userValid.typeEmployee,
                Token = userValid.token,
            });
    }
}
