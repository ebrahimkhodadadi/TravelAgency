using Microsoft.EntityFrameworkCore;
using TravelAgency.Persistence.Repositories;
using TravelAgency.Tests.Integration.Container.Api;
using TravelAgency.Persistence.Framework;
using ZiggyCreatures.Caching.Fusion;

namespace TravelAgency.Tests.Integration.Container.Persistance;

public sealed class DatabaseFixture : IDisposable
{
    private readonly TravelAgencyDbContext _context;
    private readonly TestDataGenerator _testDataGenerator;
    private readonly TimeProvider _timeProvider;

    public DatabaseFixture(string connectionString)
    {
        var factory = new TravelAgencyDbContextFactory();
        _context = factory.CreateDbContext([connectionString]);
        _context.Database.Migrate();
        _timeProvider = TimeProvider.System;

        var testContext = new TestUserContextService();
        var outboxRepository = new OutboxRepository(_context, _timeProvider);
        var fusionCache = new FusionCache(new FusionCacheOptions());
        var unitOfWork = new UnitOfWork<TravelAgencyDbContext>(_context, testContext, outboxRepository, fusionCache);
        _testDataGenerator = new TestDataGenerator(unitOfWork);
    }

    /// <summary>
    /// Use to generate test data
    /// </summary>
    public TestDataGenerator DataGenerator => _testDataGenerator;

    /// <summary>
    /// Database context
    /// </summary>
    public TravelAgencyDbContext Context => _context;

    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }
}
