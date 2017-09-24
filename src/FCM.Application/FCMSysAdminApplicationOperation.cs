using System;
using Framework.Application;
using Framework.Application.Contracts;
using Framework.DomainModel.Repositories;
using FCM.DomainModel.Entities;
using FCM.DomainModel.Repositories;
using AutoMapper;

namespace FCM.Application
{
    public abstract class FCMSysAdminApplicationOperation<TInput, TOutput, TUnitOfWork> : FCMApplicationOperation<TInput, TOutput, TUnitOfWork>
        where TUnitOfWork : IUnitOfWork
    {
        public FCMSysAdminApplicationOperation(IMapper mapper, ApplicationOperationDependecyContext<TUnitOfWork, IFCMRepositoryContainer, IFCMRepositoryContainerFactory> dependecyContext) : base(mapper, dependecyContext)
        {
        }

        protected override bool Authorize(ApplicationOperationContext<InputEnvelop<TInput>, TUnitOfWork, Principal, IFCMRepositoryContainer> applicationOperationContext)
        {
            return applicationOperationContext.IdentityId.Application.IsSysAdmin;
        }
    }
}
