using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Amazon;
using Amazon.S3;
using Microsoft.Extensions.Options;
using AssociationsClean.Infrastructure.Data;
using AssociationsClean.Infrastructure.Persistence;
using AssociationsClean.Infrastructure.Features.Categories;
using AssociationsClean.Application.Shared.Abstractions.Data;
using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Application.Features.Categories;
using AssociationsClean.Application.Shared.Abstractions.Storage;
using AssociationsClean.Infrastructure.Services;
using AssociationsClean.Application.Features.Associations;
using AssociationsClean.Infrastructure.Features.Associations;
using AssociationsClean.Application.Features.AssociationsHistory;
using Microsoft.Extensions.Caching.Memory;


namespace AssociationsClean.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
              this IServiceCollection services,
              IConfiguration configuration)
        {

            var host= Environment.GetEnvironmentVariable("PGHOST") ??
                                    configuration.GetConnectionString("PGHOST");

            var port= Environment.GetEnvironmentVariable("PGPORT") ??
                                    configuration.GetConnectionString("PGPORT");

            var username = Environment.GetEnvironmentVariable("PGUSER") ??
                                    configuration.GetConnectionString("PGUSER");

            var password= Environment.GetEnvironmentVariable("PGPASSWORD") ??
                                    configuration.GetConnectionString("PGPASSWORD");

            var database= Environment.GetEnvironmentVariable("PGDATABASE") ??
                                    configuration.GetConnectionString("PGDATABASE");

            var connectionString = ($"Host={host};Port={port};Database={database};Username={username};Password={password}");

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddSingleton<ISqlConnectionFactory>(_ =>
                new SqlConnectionFactory(connectionString));

            services.AddMemoryCache();

            //REPOSITORIES
            services.AddScoped<ICategoryCommandRepository,CategoryCommandRepository>();
            services.AddScoped<CategoryQueryRepository>();
            services.AddScoped<ICategoryQueryRepository>(provider =>
                new CategoryQueryRepositoryCached(
                    provider.GetRequiredService<CategoryQueryRepository>(),
                    provider.GetRequiredService<IMemoryCache>()
                )
            );

            services.AddScoped<IAssociationCommandRepository, AssociationCommandRepository>();
            services.AddScoped<IAssociationQueryRepository, AssociationQueryRepository>();

            services.AddScoped<IAssociationHistoryCommandRepository, AssociationsHistoryCommandRepository>();
            services.AddScoped<IAssociationHistoryQueryRepository, AssociationsHistoryQueryRepository>();


            //DB
            services.AddScoped(sp=> (IUnitOfWork)sp.GetRequiredService<AppDbContext>());

            //S3
            services.AddSingleton<IAmazonS3>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<S3Settings>>().Value;

                var config = new AmazonS3Config 
                { 
                    RegionEndpoint = RegionEndpoint.GetBySystemName(settings.RegionName) 
                };


                var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
                var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");

                return new AmazonS3Client(accessKey,secretKey,config);
            });
            services.AddScoped<IStorageService, S3StorageService>();

            return services;
        }
    }
}
