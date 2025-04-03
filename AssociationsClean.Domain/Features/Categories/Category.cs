namespace AssociationsClean.Domain.Features.Categories
{
    public sealed class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; } 
        public string? Photo { get; private set; }

        public Category(string name, string? photo)
        {
            Name = name;
            Photo = photo;
        }

        private Category() { }
    }
}
