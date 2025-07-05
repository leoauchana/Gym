using Web_Domain.Common;

namespace Web_Domain.Entities;

public class Client : EntityBase
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public int Dni { get; set; }
    public string? Domicile { get; set; }
    public int Age { get; set; }
    public Inscription? Inscription { get; set; }
}
