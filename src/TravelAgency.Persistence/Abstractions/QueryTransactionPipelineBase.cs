using MediatR;
using TravelAgency.Domain.Common.Results.Abstractions;
using TravelAgency.Persistence.Framework;
using static Microsoft.EntityFrameworkCore.QueryTrackingBehavior;

namespace TravelAgency.Persistence.Abstractions;

public abstract class QueryTransactionPipelineBase<TQueryResponse>
    where TQueryResponse : IResult
{
    protected readonly IUnitOfWork<TravelAgencyDbContext> UnitOfWork;

    protected QueryTransactionPipelineBase(IUnitOfWork<TravelAgencyDbContext> unitOfWork)
    {
        UnitOfWork = unitOfWork;
        UnitOfWork
            .Context
            .ChangeTracker
            .QueryTrackingBehavior = NoTracking;
    }

    protected async Task<TQueryResponse> BeginTransactionAsync(RequestHandlerDelegate<TQueryResponse> next, CancellationToken cancellationToken)
    {
        using var transaction = await UnitOfWork.BeginTransactionAsync(cancellationToken);
        return await next();
    }
}