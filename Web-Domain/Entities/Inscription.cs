using Web_Domain.Common;

namespace Web_Domain.Entities;

public class Inscription : EntityBase
{
    public DateTime? InscriptionDate { get; set; }
    public List<Pay>? Pays { get; set; }
    public int ClientId { get; set; }
    public Client? Client { get; set; }
}
