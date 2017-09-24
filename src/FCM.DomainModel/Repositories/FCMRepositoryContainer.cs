using Framework.DomainModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCM.DomainModel.Repositories
{
    public class FCMRepositoryContainer : IRepositoryContainer
    {
        public IExternalSystemRepository ApplicationRepository
        {
            get;
            private set;
        }

        public IComponentRepository UIComponentRepository
        {
            get;
            private set;
        }

        public FCMRepositoryContainer(IExternalSystemRepository applicationRepository, IComponentRepository uiComponentRepository)
        {
            this.ApplicationRepository = applicationRepository;
            this.UIComponentRepository = uiComponentRepository;
        }
    }
}
