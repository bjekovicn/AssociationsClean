using AssociationsClean.Domain.Abstractions;
using MediatR;

namespace AssociationsClean.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
