using AssociationsClean.Application.Features.Associations.GetRandomAssociationsByCategoryIds;
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

        public async Task<IReadOnlyList<AssociationWithCategory>> GetOldestAnsweredAssociationsByCategoryIdsAsync(Guid userUuid, IEnumerable<int> categoryIds, int count)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            var sql = @"
                SELECT a.""Id"", a.""Description"", a.""CategoryId"", c.""Name"" as ""CategoryName""
                FROM public.""Associations"" a
                JOIN public.""Categories"" c ON a.""CategoryId"" = c.""Id""
                JOIN public.""AssociationsHistory"" ah ON a.""Id"" = ah.""AssociationId""
                WHERE ah.""UserUuid"" = @UserUuid
                AND (@CategoryIds::int[] IS NULL OR a.""CategoryId"" = ANY(@CategoryIds))
                ORDER BY ah.""AnsweredAt"" ASC
                LIMIT @Count";

            var parameters = new
            {
                UserUuid = userUuid,
                CategoryIds = categoryIds.ToArray(),
                Count = count
            };

            var result = await connection.QueryAsync<AssociationWithCategory>(sql, parameters);
            return result.AsList();
        }
    }
}
