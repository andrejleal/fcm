using Framework.DomainModel.Entities;
using Framework.DomainModel.Repositories;
using Framework.Infrastructure.Repositories.EF;

namespace Framework.Repositories.EF
{
    public class EFAggregateRootRepository<TEntity> : EFBaseRepository<TEntity>, IAggregateRootRepository<TEntity>
        where TEntity : DomainEntity
    {
        public EFAggregateRootRepository(EFBaseDbContext context) : base(context)
        {
            
        }

        public virtual void Add(TEntity entity)
        {
            this.context.Set<TEntity>().Add(entity);
            this.context.SaveChanges();
        }
    }
}
