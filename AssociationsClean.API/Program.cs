using Amazon.S3;
using Associations.API.Extensions;
using AssociationsClean.Application;
using AssociationsClean.Application.Shared.Abstractions.Storage;
using AssociationsClean.Infrastructure;
using AssociationsClean.Infrastructure.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueCountLimit = 10000; 
    options.MultipartBodyLengthLimit = 104857600; 
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.Configure<S3Settings>(builder.Configuration.GetSection("AWS"));


var app = builder.Build();

app.Use(async (context, next) =>
{
    if (context.Request.HasFormContentType)
    {
        foreach (var key in context.Request.Form.Keys)
        {
            Console.WriteLine($"Form key: {key}");
        }
    }
    await next();
});


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


app.UseHttpsRedirection();

app.UseCustomExceptionHandler();

app.UseCustomExceptionHandler();

app.MapControllers();

app.Run();
