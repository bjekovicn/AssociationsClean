using AssociationsClean.Domain.Shared.Abstractions;
using MediatR;

namespace AssociationsClean.Application.Shared.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
