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

internal sealed class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.ToTable(TableName.Bill, SchemaName.TravelAgency);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion<BillIdConverter, BillIdComparer>()
            .HasColumnType(ColumnType.Char(UlidCharLenght));

        builder.Property(c => c.Status)
            .HasConversion<BillStatusConverter>()
            .HasColumnName(nameof(BillStatus))
            .HasDefaultValue(BillStatus.InProgress)
            .HasColumnType(ColumnType.VarChar(LongestOf<BillStatus>()));

        builder.Property(p => p.CustomerId)
            .HasConversion<CustomerIdConverter, CustomerIdComparer>()
            .HasColumnType(ColumnType.Char(UlidCharLenght));

        builder
            .ConfigureAuditableEntity()
            .ConfigureSoftDeletableEntity();

        builder.OwnsMany(
            p => p.Payments,
            paymentNavigationBuilder =>
            {
                paymentNavigationBuilder.ToTable(TableName.Payment, SchemaName.TravelAgency);

                paymentNavigationBuilder
                    .WithOwner()
                    .HasForeignKey(nameof(BillId));

                paymentNavigationBuilder
                    .Property<Ulid>(ShadowColumnName.Id)
                    .HasConversion<UlidToStringConverter>();
                paymentNavigationBuilder
                    .HasKey(ShadowColumnName.Id);

                paymentNavigationBuilder.Property(p => p.Price)
                    .HasConversion<MoneyConverter, MoneyComparer>()
                    .HasColumnName("Price");

                paymentNavigationBuilder.Property(p => p.TransferId)
                .HasConversion<PaymentIdConverter, PaymentIdComparer>()
                .HasColumnType(ColumnType.Char(UlidCharLenght));

                paymentNavigationBuilder
                .WithOwner()
                .HasForeignKey(nameof(TravelId));

                paymentNavigationBuilder.Property(c => c.PaymentType)
                .HasConversion<PaymentTypeConverter>()
                .HasColumnName(nameof(PaymentType))
                .HasColumnType(ColumnType.VarChar(LongestOf<PaymentType>()));

                paymentNavigationBuilder.Property(x => x.Description);
            });

        builder.OwnsMany(
            p => p.Travels,
            TravelNavigationBuilder =>
            {
                TravelNavigationBuilder.ToTable(TableName.Travel, SchemaName.TravelAgency);

                TravelNavigationBuilder
                    .WithOwner()
                    .HasForeignKey(nameof(BillId));

                TravelNavigationBuilder
                    .Property<Ulid>(ShadowColumnName.Id)
                    .HasConversion<UlidToStringConverter>();
                TravelNavigationBuilder
                    .HasKey(ShadowColumnName.Id);

                TravelNavigationBuilder.OwnsOne(o => o.Direction, options =>
                {
                    options.Property(p => p.Origin);
                    options.Property(p => p.Destination);
                });

                TravelNavigationBuilder.Property(entity => entity.Start)
                .HasColumnType(ColumnType.DateTimeOffset(2));

                TravelNavigationBuilder.Property(c => c.Type)
                .HasConversion<TravelTypeConverter>()
                .HasColumnName(nameof(TravelType))
                .HasDefaultValue(BillStatus.InProgress)
                .HasColumnType(ColumnType.VarChar(LongestOf<TravelType>()));

                TravelNavigationBuilder.Property(p => p.Price)
                .HasConversion<MoneyConverter, MoneyComparer>()
                .HasColumnName("Price");
            });
    }
}