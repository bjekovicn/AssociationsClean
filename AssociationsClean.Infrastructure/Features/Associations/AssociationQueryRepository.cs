using AssociationsClean.Domain.Features.Associations;
using AssociationsClean.Application.Features.Associations;
using AssociationsClean.Application.Shared.Abstractions.Data;
using Dapper;

namespace AssociationsClean.Infrastructure.Features.Associations
{
    internal sealed class AssociationQueryRepository : IAssociationQueryRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public AssociationQueryRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();
            var sql = "SELECT EXISTS(SELECT 1 FROM public.\"Associations\" WHERE \"Id\" = @Id)";
            return await connection.ExecuteScalarAsync<bool>(sql, new { Id = id });
        }

        public async Task<bool> ExistsAsync(int categoryId, string associationName)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();
            var sql = "SELECT EXISTS(SELECT 1 FROM public.\"Associations\" WHERE \"CategoryId\" = @CategoryId AND \"Name\" = @AssociationName)";
            return await connection.ExecuteScalarAsync<bool>(sql, new { CategoryId = categoryId, AssociationName = associationName });
        }

        public async Task<IReadOnlyList<Association>> GetAllAsync()
        {
            using var connection = _sqlConnectionFactory.CreateConnection();
            var sql = "SELECT * FROM public.\"Associations\""; 

            var result = await connection.QueryAsync<Association>(sql);
            return result.AsList();
        }

        public async Task<IReadOnlyList<Association>> GetByCategoryIdAsync(int categoryId)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();
            var sql = "SELECT * FROM public.\"Associations\" WHERE \"CategoryId\" = @CategoryId";

            var result = await connection.QueryAsync<Association>(sql, new { CategoryId = categoryId });
            return result.AsList();
        }

        public async Task<Association?> GetByIdAsync(int id)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();
            var sql = "SELECT * FROM public.\"Associations\" WHERE \"Id\" = @Id";

            return await connection.QueryFirstOrDefaultAsync<Association>(sql, new { Id = id });
        }

        public async Task<IReadOnlyList<Association>> GetRandomByCategoryIdsAsync(int count, List<int> categoryIds)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            var sql = @"
                SELECT * 
                FROM public.""Associations"" 
                WHERE ""CategoryId"" = ANY(@CategoryIds)
                ORDER BY RANDOM()
                LIMIT @Count;
            ";

            var result = await connection.QueryAsync<Association>(sql, new
            {
                CategoryIds = categoryIds.ToArray(),
                Count = count
            });

            return result.AsList();
        }
    }
}
