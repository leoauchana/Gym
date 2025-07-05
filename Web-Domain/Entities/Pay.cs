using Web_Domain.Common;

namespace Web_Domain.Entities;

public class Pay : EntityBase
{
    public DateTime? PayDate { get; set; }
    public Guid FeeId { get; set; }
    public Fee? Fee { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public Guid InscriptionId { get; set; }
    public Inscription? Inscription { get; set; }
}
