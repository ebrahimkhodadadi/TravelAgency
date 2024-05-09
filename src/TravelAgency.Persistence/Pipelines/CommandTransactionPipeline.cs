using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Domain.Common.Results.Abstractions;
using TravelAgency.Persistence.Abstractions;
using TravelAgency.Persistence.Framework;

namespace TravelAgency.Persistence.Pipelines;

public sealed class CommandTransactionPipeline<TCommandRequest, TCommandResponse>(IUnitOfWork<TravelAgencyDbContext> unitOfWork)
    : CommandTransactionPipelineBase<TCommandResponse>(unitOfWork),
      IPipelineBehavior<TCommandRequest, TCommandResponse>
        where TCommandRequest : class, IRequest<TCommandResponse>, ICommand
        where TCommandResponse : class, IResult
{
    public async Task<TCommandResponse> Handle(TCommandRequest command, RequestHandlerDelegate<TCommandResponse> next, CancellationToken cancellationToken)
    {
        var executionStrategy = UnitOfWork.CreateExecutionStrategy();
        return await executionStrategy.ExecuteAsync(cancellationToken => BeginTransactionAsync(next, cancellationToken), cancellationToken);
    }
}