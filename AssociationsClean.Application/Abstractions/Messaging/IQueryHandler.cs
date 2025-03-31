using AssociationsClean.Domain.Abstractions;
using MediatR;

namespace AssociationsClean.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
