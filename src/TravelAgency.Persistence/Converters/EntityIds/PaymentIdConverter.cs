﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAgency.Domain.Billing;

namespace TravelAgency.Persistence.Converters.EntityIds;

public sealed class PaymentIdConverter : ValueConverter<PaymentId, string>
{
    public PaymentIdConverter() : base(id => id.Value.ToString(), ulid => PaymentId.Create(Ulid.Parse(ulid))) { }
}

public sealed class PaymentIdComparer : ValueComparer<PaymentId>
{
    public PaymentIdComparer() : base((id1, id2) => id1!.Value == id2!.Value, id => id.Value.GetHashCode()) { }
}