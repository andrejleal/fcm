using Framework.Infrastruture;

namespace FCM.Repositories.EF.SQLServer
{
    public class EFSQLServerFCMUnitOfWorkFactory : EFFCMUnitOfWorkFactory
    {
        public EFSQLServerFCMUnitOfWorkFactory(IConfigurationProvider configurationProvider): base(new SQLServerFCMContextFactory(configurationProvider))
        {
        }
    }
}
