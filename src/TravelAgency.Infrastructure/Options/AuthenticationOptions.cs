﻿namespace TravelAgency.Infrastructure.Options;

public sealed class AuthenticationOptions
{
    /// <summary>
    /// Who is token generated from (for instance some authentication service)
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    // / <summary>
    /// Who is token intended for (for instance some application)
    /// </summary>
    public string Audience { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public int AccessTokenExpirationInMinutes { get; set; }
    public int RefreshTokenExpirationInDays { get; set; }
    public int TwoFactorTokenExpirationInSeconds { get; set; }
    public int ClockSkew { get; set; }
}
