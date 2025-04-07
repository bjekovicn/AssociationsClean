
using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace AssociationsClean.Application.Features.Categories.UpdateCategory
{
    public sealed record UpdateCategoryCommand(int Id, string Name,string? Photo) : ICommand;
}
