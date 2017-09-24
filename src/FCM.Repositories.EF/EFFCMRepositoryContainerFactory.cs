
using System;
using Framework.DomainModel.Repositories;
using FCM.Repositories.EF;
using Framework.Infrastructure.Repositories.EF;

namespace FCM.DomainModel.Repositories
{
    public class EFFCMRepositoryContainerFactory : IFCMRepositoryContainerFactory
    {
        public IFCMRepositoryContainer GetInstance(IUnitOfWork unitOfWork)
        {
           var context = ((EFUnitOfWork<FCMContext>)unitOfWork).Context;
           return new EFFCMRepositoryContainer(
                new EFExternalSystemRepository(context),
                new EFComponentRepository(context)
           );
        }
    }
}
