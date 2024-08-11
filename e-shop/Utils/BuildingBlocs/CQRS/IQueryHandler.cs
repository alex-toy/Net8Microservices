﻿using MediatR;

namespace BuildingBlocs.CQRS;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> 
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{
}
