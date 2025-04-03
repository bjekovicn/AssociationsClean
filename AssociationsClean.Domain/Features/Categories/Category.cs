namespace AssociationsClean.Domain.Features.Categories
{
    public sealed class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Photo { get; set; }
    }

}
