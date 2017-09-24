using System;
using System.Collections.Generic;
using Framework.DomainModel.Entities;
using Framework.Infrastructure.Repositories.EF;
using FCM.DomainModel.Repositories;
using System.Linq;

namespace Framework.Repositories.EF
{
    public class EFBaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : DomainEntity
    {
        protected EFBaseDbContext context;

        public EFBaseRepository(EFBaseDbContext context)
        {
            this.context = context;
        }

        public void DeleteById(Guid id)
        {
            var entity = this.context.Set<TEntity>().First( o => o.AppId == id);
            this.context.Set<TEntity>().Remove(entity);
            this.context.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.context.Set<TEntity>().ToList();
        }

        public TEntity GetById(Guid id)
        {
            return this.context.Set<TEntity>().FirstOrDefault(o => o.AppId == id);
        }

        public TEntity Update(TEntity entity)
        {
            throw new Exception();
        }
    }
}
