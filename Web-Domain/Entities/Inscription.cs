using Web_Domain.Common;

namespace Web_Domain.Entities;

public class Inscription : EntityBase
{
    public int InscriptionNumber { get; set; }
    public DateTime? InscriptionDate { get; set; }
    public List<Pay>? Pays { get; set; }
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
}
