using Microsoft.EntityFrameworkCore;

namespace TravelAgency.Persistence.Framework;

public sealed class TravelAgencyDbContext : DbContext
{
    public TravelAgencyDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}