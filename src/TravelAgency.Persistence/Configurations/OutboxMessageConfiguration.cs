using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Persistence.Converters;
using TravelAgency.Persistence.Converters.Enums;
using static TravelAgency.Domain.Common.Utilities.EnumUtilities;
using static TravelAgency.Persistence.Constants.Constants;
using static TravelAgency.Persistence.Constants.Constants.Number;
using TravelAgency.Infrastructure.Outbox;

namespace TravelAgency.Persistence.Configurations;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable(TableName.OutboxMessage, SchemaName.Outbox);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<UlidToStringConverter>()
            .HasColumnType(ColumnType.Char(UlidCharLenght));

        builder.Property(x => x.Type)
            .HasColumnType(ColumnType.VarChar(100));

        builder.Property(x => x.Content)
            .HasColumnType(ColumnType.VarChar(5000));

        builder.Property(x => x.Error)
            .HasColumnType(ColumnType.VarChar(8000));

        builder.Property(p => p.ExecutionStatus)
            .HasConversion<ExecutionStatusConverter>()
            .HasColumnType(ColumnType.VarChar(LongestOf<ExecutionStatus>()))
            .HasDefaultValue(ExecutionStatus.InProgress)
            .IsRequired(true);

        builder
            .HasIndex(x => x.ExecutionStatus)
            .HasDatabaseName($"IX_{nameof(OutboxMessage)}_{nameof(OutboxMessage.ExecutionStatus)}")
            .HasFilter("[ExecutionStatus] = 'InProgress'");
    }
}
