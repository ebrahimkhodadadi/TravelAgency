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

internal sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable(TableName.Payment, SchemaName.TravelAgency);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion<PaymentIdConverter, PaymentIdComparer>()
            .HasColumnType(ColumnType.Char(UlidCharLenght));

        builder.Property(p => p.Price)
            .HasConversion<MoneyConverter, MoneyComparer>()
            .HasColumnName("Price");

        builder.Property(p => p.TransferId)
        .HasConversion<PaymentIdConverter, PaymentIdComparer>()
        .HasColumnType(ColumnType.Char(UlidCharLenght));

        builder.Property(c => c.PaymentType)
        .HasConversion<PaymentTypeConverter>()
        .HasColumnName(nameof(PaymentType))
        .HasColumnType(ColumnType.VarChar(LongestOf<PaymentType>()));

        builder.Property(x => x.Description);
    }
}