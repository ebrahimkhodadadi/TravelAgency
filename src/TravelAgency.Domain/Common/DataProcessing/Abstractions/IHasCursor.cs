namespace TravelAgency.Domain.Common.DataProcessing.Abstractions;

public interface IHasCursor
{
    public Ulid Id { get; }
}