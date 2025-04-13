using AssociationsClean.Domain.Features.Associations;

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
        public ICollection<Association> Associations { get; private set; } = new List<Association>();

        private Category() { }

        public void ChangeName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("Category name cannot be empty.", nameof(newName));
            }

            if (newName.Length < 3)
            {
                throw new ArgumentException("Category name must be at least 3 characters long.", nameof(newName));
            }

            Name = newName; 
        }

        public void ChangePhoto(string? newPhoto)
        {
            if (newPhoto != null && !Uri.IsWellFormedUriString(newPhoto, UriKind.Absolute))
            {
                throw new ArgumentException("The photo URL is not valid.", nameof(newPhoto));
            }

            Photo = newPhoto;
        }
    }
}
