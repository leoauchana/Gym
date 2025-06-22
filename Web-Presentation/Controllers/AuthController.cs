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
    [HttpGet]
    public async Task<IActionResult> Login(EmployeeDto employeeDto)
    {
        await _authService.LoginEmployee(employeeDto);
        return null;
    }
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutEmployee();
        return null;
    }
}
