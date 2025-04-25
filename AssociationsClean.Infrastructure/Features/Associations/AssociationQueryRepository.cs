using AssociationsClean.Domain.Features.Associations;
using AssociationsClean.Application.Features.Associations;
using AssociationsClean.Application.Shared.Abstractions.Data;
using Dapper;
using AssociationsClean.Application.Features.Associations.GetRandomAssociationsByCategoryIds;

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

        public async Task<IReadOnlyList<AssociationWithCategory>> GetRandomByCategoryIdsAsync(int count, List<int>? categoryIds)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            var useAllCategories = categoryIds == null || categoryIds.Count == 0;

            var sql = @"
                SELECT 
                    a.""Id"", 
                    a.""Name"", 
                    a.""Description"",
                    a.""CategoryId"", 
                    c.""Name"" AS ""CategoryName""
                FROM public.""Associations"" a
                INNER JOIN public.""Categories"" c ON a.""CategoryId"" = c.""Id""
                /**where**/
                ORDER BY RANDOM()
                LIMIT @Count;
            ";

            var builder = new SqlBuilder();

            if (!useAllCategories)
            {
                builder.Where(@"a.""CategoryId"" = ANY(@CategoryIds)");
            }

            var template = builder.AddTemplate(sql, new
            {
                CategoryIds = categoryIds?.ToArray() ?? Array.Empty<int>(),
                Count = count
            });

            var result = await connection.QueryAsync<AssociationWithCategory>(template.RawSql, template.Parameters);
            return result.AsList();
        }

        public async Task<IReadOnlyList<AssociationWithCategory>> GetRandomUnansweredByCategoryIdsAsync(int count, List<int>? categoryIds, IReadOnlyList<int> answeredIds)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            // GET ONLY UNANSWERED ASSOCIATION IDS FOR PASSED CATEGORIES
            var idsSql = @"
                SELECT a.""Id""
                FROM public.""Associations"" a
                /**where**/
            ";

            var builder = new SqlBuilder();
            if (categoryIds?.Count > 0)
                builder.Where(@"a.""CategoryId"" = ANY(@CategoryIds)");
            if (answeredIds.Count > 0)
                builder.Where(@"a.""Id"" != ALL(@AnsweredIds)");

            var idsTemplate = builder.AddTemplate(
                idsSql, 
                new { 
                    CategoryIds = categoryIds?.ToArray() ?? Array.Empty<int>(),
                    AnsweredIds = answeredIds.ToArray()
                });

            var resultIDs = await connection.QueryAsync<int>(idsTemplate.RawSql, idsTemplate.Parameters);
            var allCandidateIds= resultIDs.ToList();

            if (!allCandidateIds.Any())
            { 
                return new List<AssociationWithCategory>();
            }

            // SHUFFLE IDS AND GET FULL ASSOCIATIONS DATA
            var random = new Random();
            var pickedIds = allCandidateIds.OrderBy(x => random.Next()).Take(count).ToArray();

            var sql = @"
                SELECT a.""Id"", a.""Name"", a.""Description"", a.""CategoryId"", c.""Name"" as ""CategoryName""
                FROM public.""Associations"" a
                INNER JOIN public.""Categories"" c ON a.""CategoryId"" = c.""Id""
                WHERE a.""Id"" = ANY(@PickedIds)
            ";

            var result = await connection.QueryAsync<AssociationWithCategory>(sql, new { PickedIds = pickedIds });
            return result.AsList();
        }


    }
}
