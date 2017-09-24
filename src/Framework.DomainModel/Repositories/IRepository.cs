using Framework.DomainModel.Entities;
using System;
using System.Collections.Generic;

namespace FCM.DomainModel.Repositories
{
    public interface IRepository<T>
        where T: DomainEntity
    {
        T Update(T entity);
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        void DeleteById(Guid id);
    }
}
