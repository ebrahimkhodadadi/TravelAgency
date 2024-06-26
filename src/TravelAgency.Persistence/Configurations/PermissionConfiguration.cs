﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Domain.Users.Enumerations;
using static TravelAgency.Domain.Common.Utilities.EnumUtilities;
using static TravelAgency.Persistence.Constants.Constants;

namespace TravelAgency.Persistence.Configurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableName.Permission, SchemaName.Master);

        builder.HasKey(p => p.Id);

        builder.Property(r => r.Id)
            .HasColumnType(ColumnType.TinyInt);

        builder.Property(r => r.Name)
            .HasColumnType(ColumnType.VarChar(128));

        var permissionsFromEnumeration = Permission.GetNames();
        var permissionsFromEnum = GetNamesOf<Domain.Enums.Permission>();

        bool areEnumPermisionsEquivalentToEnumerationPermissions =
            permissionsFromEnumeration.SetEquals(permissionsFromEnum);

        if (areEnumPermisionsEquivalentToEnumerationPermissions is false)
        {
            throw new Exception($"{nameof(Permission)} enum values are not equivalent to {nameof(Permission)} enumeration values");
        }

        //Inserting static data (data that are not related to other)
        builder.HasData(Permission.List);
    }
}
