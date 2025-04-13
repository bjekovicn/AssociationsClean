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


namespace AssociationsClean.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
              this IServiceCollection services,
              IConfiguration configuration)
        {

            string connectionString = configuration.GetConnectionString("DefaultConnection") ??
                                 throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddSingleton<ISqlConnectionFactory>(_ =>
                new SqlConnectionFactory(connectionString));

            //REPOSITORIES
            services.AddScoped<ICategoryCommandRepository,CategoryCommandRepository>();
            services.AddScoped<ICategoryQueryRepository,CategoryQueryRepository>();

            services.AddScoped<IAssociationCommandRepository, AssociationCommandRepository>();
            services.AddScoped<IAssociationQueryRepository, AssociationQueryRepository>();


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

                return new AmazonS3Client(config);
            });
            services.AddScoped<IStorageService, S3StorageService>();

            return services;
        }
    }
}
