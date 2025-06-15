using Web_Domain.Common;

namespace Web_Domain.Entities;

public class Employee : EntityBase
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Gmail { get; set; }
    public int File { get; set; }
    public TypeEmployee? TypeEmployee { get; set; }
    public string? Domicile { get; set; }
    public int Age { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}
