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

        builder.Ignore(p => p.Balance);

        builder
            .ConfigureAuditableEntity()
            .ConfigureSoftDeletableEntity();

        builder.HasMany(p => p.Payments)
            .WithOne()
            .HasForeignKey(payment => payment.BillId);     
        
        builder.HasMany(p => p.DiscountLogs)
            .WithOne()
            .HasForeignKey(discountLog => discountLog.BillId);    
        
        builder.HasMany(p => p.Travels)
            .WithOne()
            .HasForeignKey(travel => travel.BillId);
    }
}