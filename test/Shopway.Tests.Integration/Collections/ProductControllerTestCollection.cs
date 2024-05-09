using TravelAgency.Tests.Integration.Persistence;
using static TravelAgency.Tests.Integration.Constants.Constants.CollectionName;

namespace TravelAgency.Tests.Integration.Collections;

[CollectionDefinition(ProductControllerCollection)]
public sealed class ProductControllerTestCollection
    : ICollectionFixture<DatabaseFixture>,
      ICollectionFixture<DependencyInjectionContainerTestFixture>
{
    //DatabaseFixture and DependencyInjectionContainerTestFixture will be shared across all classed with this collection attribute
}