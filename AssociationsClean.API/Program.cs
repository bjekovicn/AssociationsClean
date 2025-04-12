using Amazon.S3;
using Associations.API.Extensions;
using AssociationsClean.Application;
using AssociationsClean.Application.Shared.Abstractions.Storage;
using AssociationsClean.Infrastructure;
using AssociationsClean.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.Configure<S3Settings>(builder.Configuration.GetSection("AWS"));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "OpenAPI V1");
    });
    app.ApplyMigrations();
}

app.MapPost("/images", async (
    [FromForm] IFormFile file,
    IStorageService storageService
) =>
{
    if (file == null || file.Length == 0)
        return Results.BadRequest("No file uploaded.");

    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
    using var stream = file.OpenReadStream();

    var url = await storageService.UploadFileAsync(stream, fileName, file.ContentType);

    return Results.Ok(new { FileName = fileName, Url = url });
}).DisableAntiforgery();

app.UseHttpsRedirection();

app.UseCustomExceptionHandler();

app.UseCustomExceptionHandler();

app.MapControllers();

app.Run();
