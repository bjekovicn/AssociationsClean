

using AssociationsClean.Application.Shared.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace AssociationsClean.Application.Features.Categories.CreateCategory
{
    public sealed record CreateCategoryCommand(String categoryName, IFormFile? categoryPhoto):ICommand;
}
