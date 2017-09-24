using Framework.Infrastruture;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Framework.Infrastructure.Repositories.EF
{
    public abstract class EFBaseDbContextFactory<T> : IDbContextFactory<T> where T : EFBaseDbContext
    {
        protected IConfigurationProvider configurationProvider;

        public EFBaseDbContextFactory()
        {
            
        }

        public EFBaseDbContextFactory(IConfigurationProvider configurationProvider)
        {
            this.configurationProvider = configurationProvider;
        }

        protected abstract T CreateForConfiguration(IConfigurationProvider conf);

        public T Create()
        {
            var context = this.Create(new DbContextFactoryOptions());
            return context;
        }

        public T Create(DbContextFactoryOptions options)
        {
            var context = CreateForConfiguration(this.configurationProvider);
            return context;
        }
    }
}
