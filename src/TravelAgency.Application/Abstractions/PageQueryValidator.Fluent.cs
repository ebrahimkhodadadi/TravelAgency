using FluentValidation;
using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Domain.Common.DataProcessing.Abstractions;
using TravelAgency.Domain.Common.Utilities;
using static TravelAgency.Application.Constants.Constants.Page;

namespace TravelAgency.Application.Abstractions;

public abstract class PageQueryValidator<TPageQuery, TPageResponse, TPage> : AbstractValidator<TPageQuery>
    where TPageResponse : IPageResponse
    where TPage : IPage
    where TPageQuery : IPageQuery<TPageResponse, TPage>
{
    protected PageQueryValidator()
    {
        RuleFor(query => query.Page.PageSize).Custom((pageSize, context) =>
        {
            if (AllowedPageSizes.NotContains(pageSize))
            {
                context.AddFailure(PageSize, $"{PageSize} must be in: [{AllowedPageSizes.Join(',')}]");
            }
        });
    }
}