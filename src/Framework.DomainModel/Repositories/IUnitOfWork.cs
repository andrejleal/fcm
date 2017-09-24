using System;

namespace Framework.DomainModel.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
