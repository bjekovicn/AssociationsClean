using AssociationsClean.Domain.Shared.Abstractions;

namespace AssociationsClean.Domain.Bookings;

public static class CategoryErrors
{
    public static readonly Error NotFound = new(
        "Category.Found",
        "The category with the specified identifier was not found");


}
