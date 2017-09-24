using Framework.DomainModel.Repositories;
using System;
using System.Data;

namespace Framework.Infrastructure.Repositories.EF
{
    public class EFUnitOfWork<T> : IUnitOfWork where T : EFBaseDbContext
    {
        public T Context
        {
            get;
            private set;
        }

        public string TransactionId
        {
            get;
            private set;
        }

        public EFUnitOfWork(T dbContext, IsolationLevel transationIsolationLevel)
        {
            this.InternalCtro(dbContext, transationIsolationLevel, Guid.NewGuid().ToString());
        }

        public EFUnitOfWork(T dbContext, IsolationLevel transationIsolationLevel, string transactionId)
        {
            if (string.IsNullOrWhiteSpace(transactionId))
            {
                throw new ArgumentNullException(nameof(transactionId));
            }

            this.InternalCtro(dbContext, transationIsolationLevel, transactionId);
        }

        private void InternalCtro(T dbContext, IsolationLevel transationIsolationLevel, string transactionId)
        {
            this.Context = dbContext;
            this.Context.BeginTransaction(transationIsolationLevel);
            this.TransactionId = transactionId;
        }

        public void Commit()
        {
            try
            {
                this.Context.Commit();
            }
            catch (Exception ex)
            {
                this.Context.Rollback();
                throw ex;
            }
        }

        public void Dispose()
        {
            if (this.Context.Transaction != null)
            {
                this.Context.Transaction.Dispose();
            }

            if (this.Context != null)
            {
                this.Context.Dispose();
                this.Context = null;
            }

        }
    }
}
