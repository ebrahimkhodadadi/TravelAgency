﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Domain.Users;
using TravelAgency.Domain.Users.ValueObjects;
using TravelAgency.Persistence.Converters.EntityIds;
using TravelAgency.Persistence.Converters.ValueObjects;
using static TravelAgency.Persistence.Constants.Constants;
using static TravelAgency.Persistence.Constants.Constants.Number;

namespace TravelAgency.Persistence.Configurations;

internal sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableName.User, SchemaName.Master);

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion<UserIdConverter, UserIdComparer>()
            .HasColumnType(ColumnType.Char(UlidCharLenght));

        builder.ConfigureAuditableEntity();

        builder.Property(u => u.Username)
            .HasConversion<UsernameConverter, UsernameComparer>()
            .HasColumnName(nameof(Username))
            .HasMaxLength(Username.MaxLength)
            .IsRequired(true);

        builder.Property(u => u.Email)
            .HasConversion<EmailConverter, EmailComparer>()
            .HasColumnName(nameof(Email))
            .HasMaxLength(Email.MaxLength)
            .IsRequired(true);

        builder.Property(u => u.PasswordHash)
            .HasConversion<PasswordHashConverter, PasswordHashComparer>()
            .HasColumnName(nameof(PasswordHash))
            .HasColumnType(ColumnType.NChar(PasswordHash.BytesLong))
            .IsRequired(true);

        builder.Property(u => u.RefreshToken)
            .HasConversion<RefreshTokenConverter, RefreshTokenComparer>()
            .HasColumnName(nameof(RefreshToken))
            .HasColumnType(ColumnType.VarChar(RefreshToken.Length))
            .IsRequired(false);

        builder.Property(u => u.TwoFactorTokenHash)
            .HasConversion<TwoFactorTokenHashConverter, TwoFactorTokenHashComparer>()
            .HasColumnName(nameof(TwoFactorTokenHash))
            .HasColumnType(ColumnType.NChar(TwoFactorTokenHash.BytesLong))
            .IsRequired(false);

        builder.Property(u => u.TwoFactorToptSecret)
            .HasConversion<TwoFactorToptSecretConverter, TwoFactorToptSecretComparer>()
            .HasColumnName(nameof(TwoFactorToptSecret))
            .HasColumnType(ColumnType.Char(TwoFactorToptSecret.BytesLong))
            .IsRequired(false);

        builder.Property(entity => entity.TwoFactorTokenCreatedOn)
            .HasColumnType(ColumnType.DateTimeOffset(2))
            .IsRequired(false);

        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<RoleUser>();

        builder.HasOne(u => u.Customer)
            .WithOne(c => c.User)
            .HasForeignKey<User>(u => u.CustomerId);

        //Indexes
        builder
            .HasIndex(user => user.Username)
            .HasDatabaseName($"UX_{nameof(Username)}_{nameof(Email)}")
            .IncludeProperties(user => user.Email)
            .IsUnique(true);

        builder
            .HasIndex(user => user.Email)
            .HasDatabaseName($"UX_{nameof(User)}_{nameof(Email)}")
            .IsUnique(true);
    }
}