using Framework.DomainModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCM.DomainModel.Repositories
{
    public interface IFCMRepositoryContainer : IRepositoryContainer
    {
        IExternalSystemRepository ExternalSystemRepository
        {
            get;
        }

        IComponentRepository ComponentRepository
        {
            get;
        }

    }

}
