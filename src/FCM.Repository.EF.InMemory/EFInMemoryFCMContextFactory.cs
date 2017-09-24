using Framework.Infrastructure.Repositories.EF;
using Framework.Infrastruture;

namespace FCM.Repositories.EF.InMemory
{   
    public class EFInMemoryFCMContextFactory : EFBaseDbContextFactory<FCMContext>
    {
        public EFInMemoryFCMContextFactory()
        {
            this.configurationProvider = new AppSettingsConfigurationProvider();
        }

        public EFInMemoryFCMContextFactory(IConfigurationProvider configurationProvider) : base(configurationProvider)
        {
        }

        protected override FCMContext CreateForConfiguration(IConfigurationProvider conf)
        {
            return new EFInMemoryFCMContext();
        }
    }
}
