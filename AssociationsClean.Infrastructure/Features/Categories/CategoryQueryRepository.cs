using Dapper;

using AssociationsClean.Domain.Features.Categories;
using AssociationsClean.Application.Features.Categories;
using AssociationsClean.Application.Shared.Abstractions.Data;

namespace AssociationsClean.Infrastructure.Features.Categories
{
    internal sealed class CategoryQueryRepository : ICategoryQueryRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public CategoryQueryRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();
            var sql = "SELECT COUNT(1) FROM public.\"Categories\" WHERE \"Id\" = @Id";

            var count = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });

            return count > 0;
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            using var connection = _sqlConnectionFactory.CreateConnection();
            var sql = "SELECT * FROM public.\"Categories\"";

            var result = await connection.QueryAsync<Category>(sql);
            return result.AsList();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();
            var sql = "SELECT * FROM public.\"Categories\" WHERE \"Id\" = @Id";

            return await connection.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id });
        }
    }
}
