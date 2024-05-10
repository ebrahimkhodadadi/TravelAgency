using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using TravelAgency.Domain.Billing;
using TravelAgency.Domain.Billing.Enumerations;
using TravelAgency.Domain.Billing.ValueObjects;
using TravelAgency.Persistence.Converters;
using TravelAgency.Persistence.Converters.EntityIds;
using TravelAgency.Persistence.Converters.Enums;
using static TravelAgency.Domain.Common.Utilities.EnumUtilities;
using static TravelAgency.Persistence.Constants.Constants;
using static TravelAgency.Persistence.Constants.Constants.Number;

namespace TravelAgency.Persistence.Configurations;

internal sealed class DiscountLogConfiguration : IEntityTypeConfiguration<DiscountLog>
{
    public void Configure(EntityTypeBuilder<DiscountLog> builder)
    {
        builder.ToTable(TableName.DiscountLog, SchemaName.TravelAgency);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion<TravelIdConverter, TravelIdComparer>()
            .HasColumnType(ColumnType.Char(UlidCharLenght));

        builder.Property(p => p.Price)
            .HasConversion<MoneyConverter, MoneyComparer>()
            .HasColumnName("Price");
    }
}