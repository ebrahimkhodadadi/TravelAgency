using System.Collections.Frozen;
using TravelAgency.Domain;
using TravelAgency.Domain.Common.DataProcessing.Abstractions;
using TravelAgency.Domain.Common.Utilities;

namespace TravelAgency.Application.Cache;

public static partial class ApplicationCache
{
    public static readonly FrozenDictionary<Type, IReadOnlyCollection<string>> AllowedSortPropertiesCache;

    private static FrozenDictionary<Type, IReadOnlyCollection<string>> CreateAllowedSortPropertiesCache()
    {
        Dictionary<Type, IReadOnlyCollection<string>> allowedSortPropertiesCache = [];

        var dynamicSortByTypes = AssemblyReference.Assembly
            .GetTypesWithAnyMatchingInterface(i => i.Name.Contains(nameof(IDynamicSortBy)))
            .Where(type => type.IsInterface is false);

        foreach (var type in dynamicSortByTypes)
        {
            var typeAllowedSortByProperties = type!.GetProperty(nameof(IDynamicSortBy.AllowedSortProperties))
                !.GetValue(null) as IReadOnlyCollection<string>;
            allowedSortPropertiesCache.TryAdd(type, typeAllowedSortByProperties!);
        }

        return allowedSortPropertiesCache.ToFrozenDictionary();
    }
}