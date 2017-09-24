using FCM.DomainModel.Repositories;
using Framework.DomainModel.Entities;

namespace Framework.DomainModel.Repositories
{
    public interface IAggregateRootRepository<T> : IRepository<T>
        where T : DomainEntity
    {
        void Add(T entity);
    }
}
