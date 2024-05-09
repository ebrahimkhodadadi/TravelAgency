using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using static TravelAgency.Persistence.Constants.Constants.Connection;

namespace TravelAgency.Persistence.Framework;

public sealed class TravelAgencyDbContextFactory : IDesignTimeDbContextFactory<TravelAgencyDbContext>
{
    public TravelAgencyDbContext CreateDbContext(string[]? args = null)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile(ConnectionStringJsonFile).Build();

        var optionsBuilder = new DbContextOptionsBuilder<TravelAgencyDbContext>();

        if (args is not null && args.Contains(TestConnection) is true)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString(TestConnection));
        }
        else if (args is not null && args.Length is 1)
        {
            optionsBuilder.UseSqlServer(args.Single());
        }
        else
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString(DefaultConnection));
        }

        return new TravelAgencyDbContext(optionsBuilder.Options);
    }
}