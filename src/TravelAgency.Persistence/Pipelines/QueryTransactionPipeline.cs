using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Application.Abstractions;
using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Domain.Common.Results.Abstractions;
using TravelAgency.Persistence.Abstractions;
using TravelAgency.Persistence.Framework;

namespace TravelAgency.Persistence.Pipelines;

public sealed class QueryTransactionPipeline<TQueryRequest, TQueryResponse>(IUnitOfWork<TravelAgencyDbContext> unitOfWork)
    : QueryTransactionPipelineBase<TQueryResponse>(unitOfWork), IPipelineBehavior<TQueryRequest, TQueryResponse>
    where TQueryRequest : class, IRequest<TQueryResponse>, IQuery<IResponse>
    where TQueryResponse : class, IResult<IResponse>
{
    public async Task<TQueryResponse> Handle(TQueryRequest request, RequestHandlerDelegate<TQueryResponse> next, CancellationToken cancellationToken)
    {
        var executionStrategy = UnitOfWork.CreateExecutionStrategy();
        return await executionStrategy.ExecuteAsync(cancellationToken => BeginTransactionAsync(next, cancellationToken), cancellationToken);
    }
}