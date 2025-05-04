

using AssociationsClean.Domain.Shared.Abstractions;

namespace AssociationsClean.Domain.Features.Associations
{
    public static class AssociationErrors
    {
        public static readonly Error NotFound = new(
            "Association.NotFound",
            "The association with the specified identifier was not found");

        public static readonly Error DuplicateNameInCategory = new(
            "Association.DuplicateNameInCategory",
            "An association with the same name already exists in the specified category");

        public static readonly Error InvalidName = new(
            "Association.InvalidName",
            "The association name is invalid");

        public static readonly Error InvalidDescription = new(
            "Association.InvalidDescription",
            "The association description is invalid");

        public static readonly Error CategoryNotFound = new(
          "Association.CategoryNotFound",
          "The category with the specified identifier was not found");
    }
}
