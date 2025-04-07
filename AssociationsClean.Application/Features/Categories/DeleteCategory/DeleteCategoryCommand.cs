

using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace AssociationsClean.Application.Features.Categories.DeleteCategory
{
    public sealed record DeleteCategoryCommand(int Id) : ICommand;
}
