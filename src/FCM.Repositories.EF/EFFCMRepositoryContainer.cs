
using Framework.DomainModel.Repositories;

namespace FCM.DomainModel.Repositories
{
    public class EFFCMRepositoryContainer : IFCMRepositoryContainer
    {
        public IExternalSystemRepository ExternalSystemRepository
        {
            get;
            private set;
        }

        public IComponentRepository ComponentRepository
        {
            get;
            private set;
        }

        public EFFCMRepositoryContainer(IExternalSystemRepository externalSystemRepository, IComponentRepository componentRepository)
        {
            this.ExternalSystemRepository = externalSystemRepository;
            this.ComponentRepository = componentRepository;
        }
    }
}
