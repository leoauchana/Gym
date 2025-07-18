using Web_Domain.Common;
using Web_Domain.Entities;

namespace Web_Domain.Logs;

public class LogClientsRegister : EntityBase
{
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public DateTime RegisterDate { get; set; }
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
}