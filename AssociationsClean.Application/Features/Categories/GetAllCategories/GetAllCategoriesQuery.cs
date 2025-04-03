

using AssociationsClean.Application.Shared.Abstractions.Messaging;
using AssociationsClean.Domain.Features.Categories;

namespace AssociationsClean.Application.Features.Categories.GetAllCategories
{
    public sealed record GetAllCategoriesQuery: IQuery<IReadOnlyList<Category>>;
}
