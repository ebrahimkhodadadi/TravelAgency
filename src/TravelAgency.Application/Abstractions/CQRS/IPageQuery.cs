﻿using TravelAgency.Application.Abstractions;
using TravelAgency.Domain.Common.DataProcessing.Abstractions;

namespace TravelAgency.Application.Abstractions.CQRS;

/// <summary>
/// Represents the page interface
/// </summary>
/// <typeparam name="TPageResponse">The page query response type</typeparam>
/// <typeparam name="TPage">The provided offset page type</typeparam>
public interface IPageQuery<TPageResponse, TPage> : IQuery<TPageResponse>
    where TPageResponse : IPageResponse
    where TPage : IPage
{
    TPage Page { get; }
}
