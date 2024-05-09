using FluentValidation;
using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Application.Features;
using TravelAgency.Domain.Common.DataProcessing.Abstractions;
using static TravelAgency.Application.Utilities.FluentValidationUtilities;

namespace TravelAgency.Application.Abstractions;

/// <summary>
/// A generic cursor page query validator, created to encapsulate common cursor page query validation logic
/// </summary>
internal abstract class CursorPageQueryValidator<TPageQuery, TResponse, TFilter, TSortBy, TPage> : PageQueryValidator<TPageQuery, CursorPageResponse<TResponse>, TPage>
    where TResponse : IResponse
    where TFilter : IFilter
    where TSortBy : ISortBy
    where TPage : ICursorPage
    where TPageQuery : ICursorPageQuery<TResponse, TFilter, TSortBy, TPage>
{
    protected CursorPageQueryValidator()
        : base()
    {
        RuleFor(query => query.SortBy).Custom(ValidateSortBy!);
        RuleFor(query => query.Filter).Custom(ValidateFilter!);
    }
}

/// <summary>
/// A generic cursor page query validator, created to encapsulate common cursor page query validation logic
/// </summary>
internal abstract class CursorPageQueryValidator<TPageQuery, TResponse, TFilter, TSortBy, TMapping, TPage> : CursorPageQueryValidator<TPageQuery, TResponse, TFilter, TSortBy, TPage>
    where TResponse : IResponse
    where TFilter : IFilter
    where TSortBy : ISortBy
    where TMapping : IMapping
    where TPage : ICursorPage
    where TPageQuery : ICursorPageQuery<TResponse, TFilter, TSortBy, TMapping, TPage>
{
    protected CursorPageQueryValidator()
        : base()
    {
        RuleFor(query => query.Mapping).Custom(ValidateMapping!);
    }
}