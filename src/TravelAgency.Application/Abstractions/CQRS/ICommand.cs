﻿using MediatR;
using TravelAgency.Application.Abstractions;
using TravelAgency.Domain.Common.Results.Abstractions;

namespace TravelAgency.Application.Abstractions.CQRS;

/// <summary>
/// Represents the command interface
/// </summary>
public interface ICommand : IRequest<IResult>
{
}

/// <summary>
/// Represents the command interface
/// </summary>
/// <typeparam name="TResponse">The command response type.</typeparam>
public interface ICommand<out TResponse> : IRequest<IResult<TResponse>>
    where TResponse : IResponse
{
}
