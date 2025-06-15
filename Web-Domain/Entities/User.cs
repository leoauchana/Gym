using Web_Domain.Common;

namespace Web_Domain.Entities;

public class User : EntityBase
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}
