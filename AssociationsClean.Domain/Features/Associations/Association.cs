using System.Text.Json.Serialization;
using AssociationsClean.Domain.Features.Categories;

namespace AssociationsClean.Domain.Features.Associations
{
    public sealed class Association
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public int CategoryId { get; private set; }

        [JsonIgnore]
        public Category? Category { get; private set; }

        public Association(string name, string? description, int categoryId)
        {
            Name=name;
            Description=description;
            CategoryId = categoryId;
        }

        private Association() { }


        public void ChangeCategory(int newCategoryId)
        {
            CategoryId = newCategoryId;
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Association name cannot be empty.", nameof(name));

            if (name.Length < 3)
                throw new ArgumentException("Association name must be at least 3 characters long.", nameof(name));

            Name = name;
        }

        public void ChangeDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Association description cannot be empty.", nameof(description));

            Description = description;
        }
    }
}
