using Framework.DomainModel.Repositories;
using Framework.Infrastructure.Repositories.EF;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;

namespace FCM.Repositories.EF
{
    public abstract class EFFCMUnitOfWorkFactory : EFUnitOfWorkFactory<FCMContext>, IUnitOfWorkFactory<IUnitOfWork>
    {
        public EFFCMUnitOfWorkFactory(IDbContextFactory<FCMContext> dbContextFactory) : base(dbContextFactory)
        {
        }

        protected override EFUnitOfWork<FCMContext> CreateUnitOfWork(FCMContext context, IsolationLevel transationIsolationLevel)
        {
            return new EFUnitOfWork<FCMContext>(context, transationIsolationLevel);
        }

        protected override EFUnitOfWork<FCMContext> CreateUnitOfWork(FCMContext context, IsolationLevel transationIsolationLevel, string transactionId)
        {
            return new EFUnitOfWork<FCMContext>(context, transationIsolationLevel, transactionId);
        }

        IUnitOfWork IUnitOfWorkFactory<IUnitOfWork>.Create()
        {
            return this.Create(this.DefaultTransactionIsolationLevel);
        }

        IUnitOfWork IUnitOfWorkFactory<IUnitOfWork>.Create(IsolationLevel transactionIsolationLevel)
        {
            return this.Create(transactionIsolationLevel);
        }
    }
}
