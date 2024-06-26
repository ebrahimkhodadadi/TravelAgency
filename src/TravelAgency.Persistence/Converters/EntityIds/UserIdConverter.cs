﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAgency.Domain.Users;

namespace TravelAgency.Persistence.Converters.EntityIds;

public sealed class UserIdConverter : ValueConverter<UserId, string>
{
    public UserIdConverter() : base(id => id.Value.ToString(), ulid => UserId.Create(Ulid.Parse(ulid))) { }
}

public sealed class UserIdComparer : ValueComparer<UserId>
{
    public UserIdComparer() : base((id1, id2) => id1!.Value == id2!.Value, id => id.Value.GetHashCode()) { }
}