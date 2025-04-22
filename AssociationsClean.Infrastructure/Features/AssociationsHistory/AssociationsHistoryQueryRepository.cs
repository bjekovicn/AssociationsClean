using AssociationsClean.Application.Features.AssociationsHistory;
using AssociationsClean.Application.Shared.Abstractions.Data;
using Dapper;

namespace AssociationsClean.Infrastructure.Features.Associations
{
    public sealed class AssociationsHistoryQueryRepository : IAssociationHistoryQueryRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public AssociationsHistoryQueryRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IReadOnlyList<int>> GetAnsweredAssociationIdsAsync(Guid userUuid)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            var sql = @"
                SELECT ""AssociationId""
                FROM public.""AssociationsHistory""
                WHERE ""UserUuid"" = @UserUuid";

            var result = await connection.QueryAsync<int>(sql, new { UserUuid = userUuid });
            return result.AsList();
        }


    }
}
