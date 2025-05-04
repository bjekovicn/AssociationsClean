using AssociationsClean.Infrastructure.Persistence;
using AssociationsClean.Application.Features.AssociationsHistory;
using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions;

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

        public async Task AddManyAsync(Guid userUuid, IEnumerable<(int AssociationId, bool AnsweredCorrectly)> associations)
        {
            var histories = associations.Select(x => new AssociationHistory(userUuid, x.AssociationId, x.AnsweredCorrectly));
            await _dbContext.BulkInsertAsync(histories);
        }

        public async Task DeleteManyAsync(Guid userUuid, IEnumerable<int> associationIds)
        {
            var entries = await _dbContext.Set<AssociationHistory>()
                .Where(ah => ah.UserUuid == userUuid && associationIds.Contains(ah.AssociationId))
                .ToListAsync();

            if (entries.Any())
            {
                await _dbContext.BulkDeleteAsync(entries);
            }
        }
    }
}
