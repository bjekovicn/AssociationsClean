

using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace AssociationsClean.Application.Features.Categories.CreateCategory
{
    public sealed record CreateCategoryCommand(String categoryName, String? categoryPhoto):ICommand;
}
