using MediatR;
using TravelAgency.Domain.Common.Results.Abstractions;
using TravelAgency.Persistence.Framework;

namespace TravelAgency.Persistence.Abstractions;

public class CommandTransactionPipelineBase<TCommandResponse>(IUnitOfWork<TravelAgencyDbContext> unitOfWork)
    where TCommandResponse : IResult
{
    protected readonly IUnitOfWork<TravelAgencyDbContext> UnitOfWork = unitOfWork;

    protected async Task<TCommandResponse> BeginTransactionAsync(RequestHandlerDelegate<TCommandResponse> next, CancellationToken cancellationToken)
    {
        using var transaction = await UnitOfWork.BeginTransactionAsync(cancellationToken);

        var result = await next();

        if (result.IsSuccess)
        {
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        else
        {
            await transaction.RollbackAsync(cancellationToken);
        }

        return result;
    }
}