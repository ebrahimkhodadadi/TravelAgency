using Microsoft.EntityFrameworkCore;
using TravelAgency.Persistence.Repositories;
using TravelAgency.Tests.Performance.Persistence;
using TravelAgency.Persistence.Framework;
using ZiggyCreatures.Caching.Fusion;
using static TravelAgency.Persistence.Constants.Constants.Connection;

namespace TravelAgency.Tests.Performance.Persistence;

public sealed class DatabaseFixture : IDisposable
{
    private readonly TravelAgencyDbContext _context;
    private readonly TestDataGenerator _testDataGenerator;
    private readonly TimeProvider _timeProvider;

    public DatabaseFixture()
    {
        var factory = new TravelAgencyDbContextFactory();
        _context = factory.CreateDbContext([TestConnection]);
        _context.Database.Migrate();
        _timeProvider = TimeProvider.System;

        var testContext = new TestContextService();
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
