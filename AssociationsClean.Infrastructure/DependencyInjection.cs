using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using AssociationsClean.Infrastructure.Data;
using AssociationsClean.Infrastructure.Persistence;
using AssociationsClean.Domain.Features.Categories;
using AssociationsClean.Infrastructure.Features.Categories;
using AssociationsClean.Application.Shared.Abstractions.Data;
using AssociationsClean.Domain.Shared.Abstractions;

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

            services.AddScoped<ICategoryRepository,CategoryRepository>();

            services.AddScoped(sp=> (IUnitOfWork)sp.GetRequiredService<AppDbContext>());

            return services;
        }
    }
}
