using FCM.DomainModel.Entities;
using Framework.DomainModel.Repositories;
using FCM.DomainModel.Repositories;
using Framework.Application;
using System;
using Framework.Application.Contracts;
using FCM.Application.Contracts;
using AutoMapper;

namespace FCM.Application.Operations
{
    public class AddOrUpdateExternalSystemOperation : FCMSysAdminApplicationOperation<ExternalSystemDTO, Guid, IUnitOfWork>
    {
        public AddOrUpdateExternalSystemOperation(IMapper mapper, ApplicationOperationDependecyContext<IUnitOfWork, IFCMRepositoryContainer, IFCMRepositoryContainerFactory> dependecyContext) : base(mapper, dependecyContext)
        {
        }

        protected override Guid Run(ApplicationOperationContext<InputEnvelop<ExternalSystemDTO>, IUnitOfWork, Principal, IFCMRepositoryContainer> applicationOperationContext)
        {
            var entity = this.mapper.Map<ExternalSystemDTO, ExternalSystem>(applicationOperationContext.InputDTO.Data);

            var entityInDb = applicationOperationContext.RepositoryContainer.ExternalSystemRepository.GetByName(entity.Name);

            if(entityInDb != null)
            {
                applicationOperationContext.RepositoryContainer.ExternalSystemRepository.DeleteById(entityInDb.AppId);
            }

            entity.Token = this.DependecyContext.AuthenticationProvider.GetSecuredToken(entity.Token);
            entity.AlternateToken = this.DependecyContext.AuthenticationProvider.GetSecuredToken(entity.AlternateToken);
            applicationOperationContext.RepositoryContainer.ExternalSystemRepository.Add(entity);
            return entity.AppId;
        }
    }
}
