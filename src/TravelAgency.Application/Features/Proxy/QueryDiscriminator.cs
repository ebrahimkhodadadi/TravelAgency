using TravelAgency.Domain.Common.Discriminators;

namespace TravelAgency.Application.Features.Proxy;

public sealed record class QueryDiscriminator(string Entity, Type PageType) : Discriminator;
