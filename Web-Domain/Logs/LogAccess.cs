using Web_Domain.Common;
using Web_Domain.Entities;

namespace Web_Domain.Logs;

public class LogAccess : EntityBase
{
    public bool isSuccess { get; set; }
    public DateTime? AccessDate { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}
