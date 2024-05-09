using TravelAgency.Tests.Integration.Container.Api;
using static TravelAgency.Tests.Integration.Container.Constants.Constants.CollectionName;

namespace TravelAgency.Tests.Integration.Container.Collections;

[CollectionDefinition(WebApplicationFactoryCollection)]
public sealed class WebApplicationFactoryTestCollection
    : ICollectionFixture<TravelAgencyApiFactory> //This will ensure that one container will be created per collection
{
}