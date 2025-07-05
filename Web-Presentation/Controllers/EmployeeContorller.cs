using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        var idEmployee = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var employeeRegister = await _employeeService.RegisterEmployee(idEmployee, employeeDto);
        if (employeeRegister == null) return BadRequest("Error al registrar el empleado");
        return Ok("Se registro el empleado correctamente");
    }
    public async Task<IActionResult> SetValueRule(double valueRule)
    {
        var idEmployee = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(idEmployee == null) return BadRequest("No se pudo obtener el ID del empleado");
        var isSuccessValueRule = await _employeeService.SetValueRule(idEmployee, valueRule);
        if (isSuccessValueRule) return BadRequest();
        return Ok("Valor de regla actualizado correctamente");
    }
}
