using Web_Domain.Entities;

namespace Web_Application.DTOs.Dashboard;

public class AccessDto
{
    public bool isSuccess { get; set; }
    public DateTime? AccessDate { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? TypeEmployee { get; set; } 
    public int File { get; set; }
}
