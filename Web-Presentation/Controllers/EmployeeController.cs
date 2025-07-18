using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web_Application.DTOs;
using Web_Application.Exceptions;
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
    [HttpPost("registerEmployee")]
    public async Task<IActionResult> Register([FromBody] EmployeeDto.EmployeeRequest employeeDto)
    {
        try
        {
            var idEmployee = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (idEmployee == null) return BadRequest("No se pudo obtener el ID del empleado");
            var employeeRegister = await _employeeService.RegisterEmployee(employeeDto);
            if (employeeRegister == null) return BadRequest("Error al registrar el empleado");
            return Ok(new
            {
                Message = "Empleado registrado con éxito",
                employeeRegister.name,
                employeeRegister.lastName,
                employeeRegister.gmail,
                employeeRegister.file,
                employeeRegister.typeEmployee                
            });
        }
        catch(BusinessConflictException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(EntityNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [Authorize(Policy = "Administrator")]
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid idEmployeeDelete)
    {
        try
        {

            var idEmployee = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (idEmployee == null) return BadRequest("No se pudo obtener el ID del empleado");
            var employeeDelete = await _employeeService.DeleteEmployee(idEmployee, idEmployeeDelete);
            if (employeeDelete == null) return BadRequest("Error al eliminar el empleado");
            return Ok(new
            {
                Message = "Empleado eliminado con éxito",
                employeeDelete
            });
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [Authorize(Policy = "Administrator")]
    [HttpPut]
    public async Task<IActionResult> Update(EmployeeDto.EmployeeRequest employeeDto)
    {
        try
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
        catch (EntityNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [Authorize(Policy = "Administrator")]
    [HttpPost("updateRule")]
    public async Task<IActionResult> SetValueRule(double valueRule)
    {
        var idEmployee = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(idEmployee == null) return BadRequest("No se pudo obtener el ID del empleado");
        var isSuccessValueRule = await _employeeService.SetValueRule(idEmployee, valueRule);
        if (isSuccessValueRule) return BadRequest();
        return Ok("Valor de regla actualizado correctamente");
    }
}
