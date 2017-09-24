using Framework.Infrastructure.Repositories.EF;
using Framework.Infrastruture;

namespace FCM.Repositories.EF.SQLServer
{   
    public class SQLServerFCMContextFactory : EFBaseDbContextFactory<FCMContext>
    {
        public SQLServerFCMContextFactory()
        {
            this.configurationProvider = new AppSettingsConfigurationProvider();
        }

        public SQLServerFCMContextFactory(IConfigurationProvider configurationProvider) : base(configurationProvider)
        {
        }

        protected override FCMContext CreateForConfiguration(IConfigurationProvider conf)
        {
            return new SQLServerFCMContext(conf.GetConnectionString("DefaultConnection"));
        }
    }
}
