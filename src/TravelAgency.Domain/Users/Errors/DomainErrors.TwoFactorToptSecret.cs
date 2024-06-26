﻿using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Users.ValueObjects;

namespace TravelAgency.Domain.Users.Errors;

public static partial class DomainErrors
{
    public static class TwoFactorToptSecretError
    {
        public static readonly Error Empty = Error.New(
            $"{nameof(TwoFactorToptSecret)}.{nameof(Empty)}",
            $"{nameof(TwoFactorToptSecret)} is empty.");

        public static readonly Error BytesLong = Error.New(
            $"{nameof(TwoFactorToptSecret)}.{nameof(BytesLong)}",
            $"{nameof(TwoFactorToptSecret)} needs to be {TwoFactorToptSecret.BytesLong} bytes long.");
    }
}