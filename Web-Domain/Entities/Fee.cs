using Web_Domain.Common;

namespace Web_Domain.Entities;

public class Fee : EntityBase
{
    public int FeeNumber { get; set; }
    public double Value { get; set; }
    public Pay? Pay { get; set; }
    public DateTime? InitialDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive  => IsActiveCurrent();
    public bool IsCancelled { get; set; } = false;
    private bool IsActiveCurrent()
    {
        return IsCancelled ? false : EndDate >= DateTime.Now;
    }
}
