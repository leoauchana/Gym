using Microsoft.AspNetCore.Mvc;
using Web_Application.DTOs;
using Web_Application.Interfaces;

namespace Web_Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeContorller : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    public EmployeeContorller(IEmployeeService employeeService)
    {
        _employeeService = employeeService;        
    }
    public async Task<IActionResult> Register(EmployeeDto employeeDto)
    {
        var employeeRegister = await _employeeService.RegisterEmployee(employeeDto);
        if (employeeRegister == null) return BadRequest("Error al registrar el empleado");
        return Ok("Se registro el empleado correctamente");
    }
}
