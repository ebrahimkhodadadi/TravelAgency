using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Domain.Users;
using TravelAgency.Persistence.Converters.EntityIds;
using static TravelAgency.Persistence.Constants.Constants;

namespace TravelAgency.Persistence.Configurations;

internal sealed class RoleUserConfiguration : IEntityTypeConfiguration<RoleUser>
{
    public void Configure(EntityTypeBuilder<RoleUser> builder)
    {
        builder.ToTable(TableName.RoleUser, SchemaName.Master);

        builder.HasKey(x => new { x.RoleId, x.UserId });

        builder.Property(x => x.UserId)
            .HasConversion<UserIdConverter, UserIdComparer>();
    }
}
