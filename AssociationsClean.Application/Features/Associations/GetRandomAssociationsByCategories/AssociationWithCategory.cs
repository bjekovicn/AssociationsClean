

namespace AssociationsClean.Application.Features.Associations.GetRandomAssociationsByCategoryIds
{
    public sealed class AssociationWithCategory
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
        public string CategoryName { get; init; } = default!;
    }
}
