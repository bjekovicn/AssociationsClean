


using AssociationsClean.Domain;
using AssociationsClean.Application.Abstractions.Messaging;

namespace Associations.Application.Features.Categories.GetCategories
{
    public sealed record GetCategoryByIdQuery(int Id):IQuery<Category>;
}
