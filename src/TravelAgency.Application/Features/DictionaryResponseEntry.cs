using TravelAgency.Application.Abstractions;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;
using TravelAgency.Domain.Common.DataProcessing.Abstractions;

namespace TravelAgency.Application.Features;

/// <summary>
/// If additional information is required for dictionary response entry, than just inherit from this record (create for instance OrderDictionaryResponseEntry)
/// </summary>
/// <param name="Id"></param>
/// <param name="BusinessKey"></param>
public record DictionaryResponseEntry(Ulid Id, IUniqueKey BusinessKey) : IResponse, IHasCursor;