using AssociationsClean.Infrastructure.Persistence;
using AssociationsClean.Application.Features.AssociationsHistory;

namespace AssociationsClean.Infrastructure.Features.Associations
{
    public sealed class AssociationsHistoryCommandRepository : IAssociationHistoryCommandRepository
    {
        private readonly AppDbContext _dbContext;

        public AssociationsHistoryCommandRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(AssociationHistory entity)
        {
            await _dbContext.Set<AssociationHistory>().AddAsync(entity);
        }


        public Task DeleteAsync(AssociationHistory entity)
        {
            _dbContext.Set<AssociationHistory>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(AssociationHistory entity)
        {
            _dbContext.Set<AssociationHistory>().Update(entity);
            return Task.CompletedTask;
        }

        public async Task AddManyAsync(Guid userUuid, IEnumerable<int> associationIds)
        {
            var histories = associationIds.Select(id => new AssociationHistory(userUuid, id));
            await _dbContext.Set<AssociationHistory>().AddRangeAsync(histories);
        }
    }
}
