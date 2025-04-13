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
            var sql = "SELECT COUNT(1) FROM public.\"Associations\" WHERE \"Id\" = @Id";

            var count = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });

            return count > 0;
        }

        public async Task<IReadOnlyList<Association>> GetAllAsync()
        {
            using var connection = _sqlConnectionFactory.CreateConnection();
            var sql = "SELECT * FROM public.\"Associations\""; 

            var result = await connection.QueryAsync<Association>(sql);
            return result.AsList();
        }

        public async Task<Association?> GetByIdAsync(int id)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();
            var sql = "SELECT * FROM public.\"Associations\" WHERE \"Id\" = @Id";

            return await connection.QueryFirstOrDefaultAsync<Association>(sql, new { Id = id });
        }
    }
}
