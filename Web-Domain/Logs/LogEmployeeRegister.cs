using Web_Domain.Common;
using Web_Domain.Entities;

namespace Web_Domain.Logs;

public class LogEmployeeRegister : EntityBase
{
    public Guid NewEmployeeId { get; set; }
    public Employee? NewEmployee { get; set; }
    public DateTime RegisterDate { get; set; }
    public string? Description { get; set; }
    public Guid? RegisterById { get; set; }
    public Employee? RegisterBy { get; set; }
    }
