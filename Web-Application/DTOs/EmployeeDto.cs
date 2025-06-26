using Web_Domain.Entities;

namespace Web_Application.DTOs;

public class EmployeeDto
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Gmail { get; set; }
    public int File { get; set; }
    public TypeEmployee? TypeEmployee { get; set; }
    public string? Domicile { get; set; }
    public int Age { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}
