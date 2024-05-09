﻿using TravelAgency.Domain.Common.BaseTypes;

namespace TravelAgency.Domain.Users.Events;

public sealed record UserRegisteredDomainEvent(Ulid Id, UserId UserId) : DomainEvent(Id)
{
    public static UserRegisteredDomainEvent New(UserId userId)
    {
        return new UserRegisteredDomainEvent(Ulid.NewUlid(), userId);
    }
}