using Dapper;
using AssociationsClean.Domain.Features.Categories;
using AssociationsClean.Domain.Bookings;
using AssociationsClean.Domain.Shared.Abstractions;
using AssociationsClean.Application.Shared.Abstractions.Data;
using AssociationsClean.Application.Shared.Abstractions.Messaging;

namespace Associations.Application.Features.Categories.GetCategories
{

    internal sealed class GetCategoryByIdQueryHandler:IQueryHandler<GetCategoryByIdQuery,Category>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetCategoryByIdQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            this._sqlConnectionFactory = connectionFactory;
        }

        public async Task<Result<Category>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            using var connection=_sqlConnectionFactory.CreateConnection();

            var sql = "SELECT * FROM public.\"Categories\" WHERE \"Id\" = @Id";


            var category = await connection.QueryFirstOrDefaultAsync<Category>(sql, new { Id = request.Id });

            if (category == null)
            {
                return Result.Failure<Category>(CategoryErrors.NotFound);
            }

            return category;
        }
    }
}
