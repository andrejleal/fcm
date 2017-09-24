using Framework.DomainModel.Repositories;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using System;
using Microsoft.EntityFrameworkCore;

namespace Framework.Infrastructure.Repositories.EF
{
    public abstract class EFUnitOfWorkFactory<TDbContext> : IUnitOfWorkFactory<EFUnitOfWork<TDbContext>>
        where TDbContext : EFBaseDbContext
    {
        protected readonly IsolationLevel DefaultTransactionIsolationLevel;
        protected readonly IDbContextFactory<TDbContext> dbContextFactory;

        public EFUnitOfWorkFactory(IDbContextFactory<TDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
            this.DefaultTransactionIsolationLevel = IsolationLevel.ReadCommitted;
        }

        protected abstract EFUnitOfWork<TDbContext> CreateUnitOfWork(TDbContext context, IsolationLevel transationIsolationLevel);
        protected abstract EFUnitOfWork<TDbContext> CreateUnitOfWork(TDbContext context, IsolationLevel transationIsolationLevel, string transactionId);

        public EFUnitOfWork<TDbContext> Create(IsolationLevel transationIsolationLevel)
        {
            var dbContext = this.dbContextFactory.Create(new DbContextFactoryOptions());

            //globally disable change tracker for queries
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var uow = this.CreateUnitOfWork(dbContext, transationIsolationLevel);

            return uow;
        }

        public EFUnitOfWork<TDbContext> Create()
        {
            return this.Create(this.DefaultTransactionIsolationLevel);
        }

        public EFUnitOfWork<TDbContext> Create(string transactionId)
        {
            return this.Create(this.DefaultTransactionIsolationLevel, transactionId);
        }

        public EFUnitOfWork<TDbContext> Create(IsolationLevel transationIsolationLevel, string transactionId)
        {
            var dbContext = this.dbContextFactory.Create(new DbContextFactoryOptions());

            //globally disable change tracker for queries
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var uow = this.CreateUnitOfWork(dbContext, transationIsolationLevel, transactionId);

            return uow;
        }
    }
}
