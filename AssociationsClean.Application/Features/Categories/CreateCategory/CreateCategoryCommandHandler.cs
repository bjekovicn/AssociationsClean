

using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace AssociationsClean.Application.Features.Categories.CreateCategory
{
    internal sealed class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand>
    {
        public CreateCategoryCommandHandler()
        {

        }

        public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
