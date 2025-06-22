using Web_Domain.Common;

namespace Web_Domain.Entities;

public class Pay : EntityBase
{
    public DateTime? PayDate { get; set; }
    public int FeeId { get; set; }
    public Fee? Fee { get; set; }
}
