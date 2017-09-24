using System;
using Framework.Application;
using Framework.Application.Contracts;
using Framework.DomainModel.Repositories;
using FCM.DomainModel.Entities;
using FCM.DomainModel.Repositories;
using AutoMapper;

namespace FCM.Application
{
    public abstract class FCMApplicationOperation<TInput, TOutput, TUnitOfWork> : ApplicationOperation<InputEnvelop<TInput>, TInput, OutputEnvelop<TOutput>, TOutput, TUnitOfWork, Principal, IFCMRepositoryContainer, IFCMRepositoryContainerFactory>
        where TUnitOfWork : IUnitOfWork
    {
        protected IMapper mapper;

        public FCMApplicationOperation(IMapper mapper, ApplicationOperationDependecyContext<TUnitOfWork, IFCMRepositoryContainer, IFCMRepositoryContainerFactory> dependecyContext) : base(dependecyContext)
        {
            this.mapper = mapper;
        }

        protected override Principal Authenticate(ApplicationOperationContext<InputEnvelop<TInput>, TUnitOfWork, Principal, IFCMRepositoryContainer> applicationOperationContext)
        {
            var principal = this.DependecyContext.AuthenticationProvider.Authenticate(applicationOperationContext.InputDTO.AuthenticationToken);
            return (principal == null) ? null : (Principal)principal;
        }

        protected override bool Authorize(ApplicationOperationContext<InputEnvelop<TInput>, TUnitOfWork, Principal, IFCMRepositoryContainer> applicationOperationContext)
        {
            return true;
        }
    }
}
