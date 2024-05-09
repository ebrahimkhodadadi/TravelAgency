using Microsoft.EntityFrameworkCore;
using TravelAgency.Persistence.Repositories;
using TravelAgency.Tests.Integration.Persistence;
using TravelAgency.Persistence.Framework;
using ZiggyCreatures.Caching.Fusion;
using static TravelAgency.Persistence.Constants.Constants.Connection;

namespace TravelAgency.Tests.Integration.Persistence;

public sealed class DatabaseFixture : IDisposable, IAsyncLifetime
{
    private readonly TravelAgencyDbContext _context;
    private readonly TestDataGenerator _testDataGenerator;
    private readonly TimeProvider _timeProvider;

    public DatabaseFixture()
    {
        var factory = new TravelAgencyDbContextFactory();
        _context = factory.CreateDbContext([TestConnection]);
        _timeProvider = TimeProvider.System;

        var pendingMigrations = _context.Database.GetPendingMigrations();

        if (pendingMigrations.Any())
        {
            _context.Database.Migrate();
        }

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

    public async Task DisposeAsync()
    {
        try
        {
            await DataGenerator.CleanDatabaseFromTestDataAsync();
        }
        catch
        {
            Console.WriteLine("CleanTestData.Integration.Api.Tests failed.");
        }
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }
}
