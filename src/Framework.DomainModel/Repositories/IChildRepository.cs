using FCM.DomainModel.Repositories;
using Framework.DomainModel.Entities;
using System;

namespace Framework.DomainModel.Repositories
{
    public interface IChildRepository<T, TParent> : IRepository<T>
        where T : ChildEntity<TParent>
        where TParent : DomainEntity
    {
        void Add(T entity, Guid parentId);
        void DeleteByParentId(Guid parentId);
    }
}
