
using AssociationsClean.Application.Shared.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace AssociationsClean.Application.Features.Categories.UpdateCategory
{
    public sealed record UpdateCategoryCommand(int Id, string? Name, IFormFile? Photo) : ICommand;

}
