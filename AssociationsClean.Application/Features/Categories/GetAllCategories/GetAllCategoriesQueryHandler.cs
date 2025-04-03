

using AssociationsClean.Domain.Features.Categories;
using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Application.Shared.Abstractions.Data;
using AssociationsClean.Application.Shared.Abstractions.Messaging;
using Dapper;

namespace AssociationsClean.Application.Features.Categories.GetAllCategories
{
    internal sealed class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, IReadOnlyList<Category>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllCategoriesQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            this._sqlConnectionFactory = connectionFactory;
        }

        public async Task<Result<IReadOnlyList<Category>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            var sql = "SELECT * FROM public.\"Categories\"";

            var categories = await connection.QueryAsync<Category>(sql);

            return Result.Success<IReadOnlyList<Category>>(categories.AsList());
        }
    }
}
