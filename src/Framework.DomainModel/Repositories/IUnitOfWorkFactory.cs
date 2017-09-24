using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Framework.DomainModel.Repositories
{
    public interface IUnitOfWorkFactory<T> where T : IUnitOfWork
    {
        T Create();
        T Create(IsolationLevel transationIsolationLevel);
    }
}
