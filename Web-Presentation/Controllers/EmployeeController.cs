using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web_Application.DTOs;
using Web_Application.Interfaces;

namespace Web_Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;        
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(EmployeeDto employeeDto)
    {
        var idEmployee = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idEmployee == null) return BadRequest("No se pudo obtener el ID del empleado");
        var employeeRegister = await _employeeService.RegisterEmployee(idEmployee, employeeDto);
        if (employeeRegister == null) return BadRequest("Error al registrar el empleado");
        return Ok(new
        {
            Message = "Empleado registrado con éxito",
            employeeRegister
        });
    }
    [Authorize(Policy = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> Delete(EmployeeDto employeeDto)
    {
        var idEmployee = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idEmployee == null) return BadRequest("No se pudo obtener el ID del empleado");
        var employeeDelete = await _employeeService.DeleteEmployee(idEmployee, employeeDto);
        if (employeeDelete == null) return BadRequest("Error al eliminar el empleado");
        return Ok(new
        {
            Message = "Empleado eliminado con éxito",
            employeeDelete
        });
    }
    [Authorize(Policy = "Admin")]
    [HttpPut]
    public async Task<IActionResult> Update(EmployeeDto employeeDto)
    {
        var idEmployee = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idEmployee == null) return BadRequest("No se pudo obtener el ID del empleado");
        var employeeUpdate = await _employeeService.UpdateEmployee(idEmployee, employeeDto);
        if (employeeUpdate == null) return BadRequest("Error al actualizar el empleado");
        return Ok(new
        {
            Message = "Empleado actualizado con éxito",
            employeeUpdate
        });
    }
    [Authorize(Policy = "Admin")]
    [HttpPost]
    public async Task<IActionResult> SetValueRule(double valueRule)
    {
        var idEmployee = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(idEmployee == null) return BadRequest("No se pudo obtener el ID del empleado");
        var isSuccessValueRule = await _employeeService.SetValueRule(idEmployee, valueRule);
        if (isSuccessValueRule) return BadRequest();
        return Ok("Valor de regla actualizado correctamente");
    }
}
