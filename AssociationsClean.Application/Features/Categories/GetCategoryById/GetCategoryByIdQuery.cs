using AssociationsClean.Application.Shared.Abstractions.Messaging;
using AssociationsClean.Domain.Features.Categories;

namespace Associations.Application.Features.Categories.GetCategories
{
    public sealed record GetCategoryByIdQuery(int Id):IQuery<Category>;
}
