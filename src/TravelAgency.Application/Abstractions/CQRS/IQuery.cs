using MediatR;
using TravelAgency.Domain.Common.Results.Abstractions;

namespace TravelAgency.Application.Abstractions.CQRS;

/// <summary>
/// Represents the query interface
/// </summary>
/// <typeparam name="TResponse">The query response type</typeparam>
public interface IQuery<out TResponse> : IRequest<IResult<TResponse>>
    where TResponse : IResponse
{
}
