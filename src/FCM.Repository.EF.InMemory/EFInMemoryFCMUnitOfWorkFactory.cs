using FCM.Repositories.EF.InMemory;
using Framework.Infrastruture;

namespace FCM.Repositories.EF.EFInMemory
{
    public class EFInMemoryFCMUnitOfWorkFactory : EFFCMUnitOfWorkFactory
    {
        public EFInMemoryFCMUnitOfWorkFactory(IConfigurationProvider configurationProvider): base(new EFInMemoryFCMContextFactory(configurationProvider))
        {
        }
    }
}
