using Web_Domain.Common;

namespace Web_Domain.Entities;

public class Fee : EntityBase
{
    public int FeeNumber { get; set; }
    public double Value { get; set; }
}
