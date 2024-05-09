using TravelAgency.Tests.Integration.Persistence;
using static TravelAgency.Tests.Integration.Constants.Constants.CollectionName;

namespace TravelAgency.Tests.Integration.Collections;

[CollectionDefinition(DatabaseCollection)]
public sealed class DatabaseFixtureCollection
    : ICollectionFixture<DatabaseFixture>
{

}
