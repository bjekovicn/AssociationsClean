using MediatR;
using AssociationsClean.Domain.Shared.Abstractions;
namespace AssociationsClean.Application.Shared.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TReponse> : IRequest<Result<TReponse>>, IBaseCommand
{
}

public interface IBaseCommand
{
}
