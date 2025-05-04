using AssociationsClean.Domain.Features.Associations;
using AssociationsClean.Infrastructure.Persistence;
using AssociationsClean.Application.Features.Associations;

namespace AssociationsClean.Infrastructure.Features.Associations
{
    internal sealed class AssociationCommandRepository : IAssociationCommandRepository
    {
        private readonly AppDbContext _dbContext;

        public AssociationCommandRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Association entity)
        {
            await _dbContext.Set<Association>().AddAsync(entity);
        }

        public Task UpdateAsync(Association entity)
        {
            _dbContext.Set<Association>().Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Association entity)
        {
            _dbContext.Set<Association>().Remove(entity);
            return Task.CompletedTask;
        }
    }
}
