namespace AssociationsClean.Domain
{
    public sealed class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Photo { get; set; }
    }

}
