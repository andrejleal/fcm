using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;

namespace Framework.Infrastructure.Repositories.EF
{
    public abstract class EFBaseDbContext : DbContext
    {
        private IDbContextTransaction currentTransaction;

        protected abstract bool SupportsTransaction { get; }
        public IDbContextTransaction Transaction { get { return this.currentTransaction; } }

        public EFBaseDbContext()
        {
        }

        public static string GetTableNameForType<T>()
        {
            return typeof(T).Name;
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IDbContextTransaction BeginTransaction(IsolationLevel transactionIsolationLevel)
        {
            if (this.SupportsTransaction)
            {
                try
                {
                    this.currentTransaction = this.Database.BeginTransaction(transactionIsolationLevel);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return this.currentTransaction;
        }

        public void Commit()
        {
            this.SaveChanges();

            try
            {
                if (this.SupportsTransaction)
                {
                    if (this.currentTransaction != null)
                    {
                        this.currentTransaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Rollback()
        {
            if (this.SupportsTransaction)
            {
                this.currentTransaction.Rollback();
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            if (this.SupportsTransaction)
            {
                if (this.currentTransaction != null)
                {
                    this.currentTransaction.Dispose();
                    this.currentTransaction = null;
                }
            }
        }
    }
}
