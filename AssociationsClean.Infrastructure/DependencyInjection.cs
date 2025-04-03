
using AssociationsClean.Infrastructure.Data;
using AssociationsClean.Infrastructure.Persistence;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AssociationsClean.Application.Shared.Abstractions.Data;

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

            return services;
        }
    }
}
