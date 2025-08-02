using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Data.SqlClient;
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
    [Authorize(Policy = "Administrator")]
    [HttpPost("registerEmployee")]
    public async Task<IActionResult> Register([FromBody] EmployeeDto.EmployeeRequest employeeDto)
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
    [Authorize(Policy = "Administrator")]
    [HttpDelete("{idEmployeeDelete}")]
    public async Task<IActionResult> Delete(Guid idEmployeeDelete)
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
    [Authorize(Policy = "Administrator")]
    [HttpPut("{idEmployeeUpdate}")]
    public async Task<IActionResult> Update(Guid idEmployeeUpdate, EmployeeDto.EmployeeRequest employeeDto)
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
    [Authorize(Policy ="Administrator")]
    [HttpGet("getAllEmployees")]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _employeeService.GetAllEmployees();
        if (employees == null) return BadRequest("Hubo un error al obtener los empleados");
        return Ok(employees);
    }
    [Authorize(Policy = "Administrator")]
    [HttpGet("getById/{idEmployee}")]
    public async Task<IActionResult> GetById(Guid idEmployee)
    {
            var employee = await _employeeService.GetEmployeeById(idEmployee);
            if (employee == null) return BadRequest("Hubo un error al buscar el empleado");
            return Ok(employee);
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
