﻿using MediatR;
using TravelAgency.Application.Abstractions;
using TravelAgency.Domain.Common.Results.Abstractions;

namespace TravelAgency.Application.Abstractions.CQRS;

/// <summary>
/// Represents the query handler interface
/// </summary>
/// <typeparam name="TQuery">The query type</typeparam>
/// <typeparam name="TResponse">The query response type</typeparam>
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, IResult<TResponse>>
    where TQuery : IQuery<TResponse>
    where TResponse : IResponse
{
}
