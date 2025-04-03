using AssociationsClean.Domain.Shared.Abstractions;
using MediatR;

namespace AssociationsClean.Application.Shared.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
