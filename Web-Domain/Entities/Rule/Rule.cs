using Web_Domain.Common;

namespace Web_Domain.Entities.Rule;

public class Rule : EntityBase
{
    public double Value { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
