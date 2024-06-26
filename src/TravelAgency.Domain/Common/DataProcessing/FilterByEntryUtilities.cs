﻿namespace TravelAgency.Domain.Common.DataProcessing;

public static class FilterByEntryUtilities
{
    public static bool ContainsInvalidFilterProperty(this IList<FilterByEntry> filterProperties, IReadOnlyCollection<string> allowedFilterProperties, out IReadOnlyCollection<string> invalidProperties)
    {
        invalidProperties = filterProperties
            .SelectMany(x => x.Predicates)
            .Select(x => x.PropertyName)
            .Except(allowedFilterProperties)
            .ToList()
            .AsReadOnly();

        return invalidProperties.Count is not 0;
    }

    public static bool ContainsOnlyOperationsFrom(this IList<FilterByEntry> filterProperties, IReadOnlyCollection<string> allowedOperations, out IReadOnlyCollection<string> invalidOperations)
    {
        invalidOperations = filterProperties
            .SelectMany(x => x.Predicates)
            .Select(x => x.Operation)
            .Except(allowedOperations)
            .ToList()
            .AsReadOnly();

        return invalidOperations.Count is not 0;
    }

    public static bool ContainsNullFilterProperty(this IList<FilterByEntry> filterProperties)
    {
        return filterProperties
            .Any(x => x is null || x.Predicates.Any(y => y is null));
    }
}