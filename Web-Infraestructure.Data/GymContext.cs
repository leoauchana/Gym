using Microsoft.EntityFrameworkCore;

namespace Web_Infraestructure.Data;

public class GymContext : DbContext
{

    public GymContext(DbContextOptions<GymContext> options) : base(options)
    {
    }
}
