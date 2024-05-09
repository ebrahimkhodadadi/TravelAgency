using System.Collections.Frozen;
using TravelAgency.Domain;
using TravelAgency.Domain.Common.DataProcessing.Abstractions;
using TravelAgency.Domain.Common.Utilities;

namespace TravelAgency.Application.Cache;

public static partial class ApplicationCache
{
    public static readonly FrozenDictionary<Type, IReadOnlyCollection<string>> AllowedFilterPropertiesCache;

    private static FrozenDictionary<Type, IReadOnlyCollection<string>> CreateAllowedFilterPropertiesCache()
    {
        Dictionary<Type, IReadOnlyCollection<string>> allowedFilterPropertiesCache = [];

        var dynamicFilterTypes = AssemblyReference.Assembly
            .GetTypesWithAnyMatchingInterface(i => i.Name.Contains(nameof(IDynamicFilter)))
            .Where(type => type.IsInterface is false);

        foreach (var type in dynamicFilterTypes)
        {
            var typeAllowedFilterProperties = type!.GetProperty(nameof(IDynamicFilter.AllowedFilterProperties))
                !.GetValue(null) as IReadOnlyCollection<string>;
            allowedFilterPropertiesCache.TryAdd(type, typeAllowedFilterProperties!);
        }

        return allowedFilterPropertiesCache.ToFrozenDictionary();
    }
}