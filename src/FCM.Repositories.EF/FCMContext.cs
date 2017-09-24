using FCM.DomainModel.Entities;
using Framework.DomainModel.Entities;
using Framework.Infrastructure.Repositories.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace FCM.Repositories.EF
{
    public abstract class FCMContext : EFBaseDbContext
    {

        #region DBSets
        public DbSet<ExternalSystem> ExternalSystem { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentProperty> ComponentProperties { get; set; }
        #endregion DBSets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.ConfigureDomainEntity<ExternalSystem>(modelBuilder);
            modelBuilder.Entity<ExternalSystem>().HasAlternateKey(p => p.AlternateToken);
            modelBuilder.Entity<ExternalSystem>().HasAlternateKey(p => p.Token);
            modelBuilder.Entity<ExternalSystem>().HasAlternateKey(p => p.Name);
            modelBuilder.Entity<ExternalSystem>().Property(p => p.AlternateToken).IsRequired();
            modelBuilder.Entity<ExternalSystem>().Property(p => p.Token).IsRequired();
            modelBuilder.Entity<ExternalSystem>().Property(p => p.Name).IsRequired();

            this.ConfigureDomainEntity<Component>(modelBuilder);
            modelBuilder.Entity<Component>().HasAlternateKey(p => p.Name);

            this.ConfigureDomainEntity<ComponentProperty>(modelBuilder);
            modelBuilder.Entity<ComponentProperty>().HasIndex(nameof(ComponentProperty.Name), nameof(ComponentProperty.Owner), nameof(ComponentProperty.ParentId)).IsUnique();
            this.ConfigureMultipleChildEntities<Component, ComponentProperty>(modelBuilder, o => o.Properties);
        }

        private void ConfigureDomainEntity<T>(ModelBuilder modelBuilder)
            where T : DomainEntity
        {
            modelBuilder.Entity<T>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<T>().HasKey(e => e.Id);
            modelBuilder.Entity<T>().Property(e => e.AppId).ValueGeneratedNever();
            modelBuilder.Entity<T>().HasAlternateKey(e => e.AppId);
            modelBuilder.Entity<T>().Property<Guid>(o => o.ConcurrencyToken).ValueGeneratedNever().IsConcurrencyToken();
        }

        private void ConfigureMultipleChildEntities<T, TChild>(ModelBuilder modelBuilder, Expression<Func<T, IEnumerable<TChild>>> childNavigationProperty)
        where T : DomainEntity
        where TChild : ChildEntity<T>
        {
            modelBuilder.Entity<T>()
                .HasMany(childNavigationProperty)
                .WithOne(e => e.Parent)
                .HasForeignKey(o=>o.ParentId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
