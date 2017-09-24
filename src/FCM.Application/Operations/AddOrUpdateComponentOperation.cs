using FCM.DomainModel.Entities;
using Framework.DomainModel.Repositories;
using FCM.DomainModel.Repositories;
using Framework.Application;
using System;
using Framework.Application.Contracts;
using FCM.Application.Contracts;
using AutoMapper;
using System.Collections.Generic;
using Framework.DomainModel.Exceptions;
using FCM.DomainModel;
using System.Linq;

namespace FCM.Application.Operations
{
    public class AddOrUpdateComponentOperation : FCMSysAdminApplicationOperation<ComponentDTO, Guid, IUnitOfWork>
    {
        private INotificationService notificationService;

        public AddOrUpdateComponentOperation(INotificationService notificationService, IMapper mapper, ApplicationOperationDependecyContext<IUnitOfWork, IFCMRepositoryContainer, IFCMRepositoryContainerFactory> dependecyContext) : base(mapper, dependecyContext)
        {
            this.notificationService = notificationService;
        }

        protected override Guid Run(ApplicationOperationContext<InputEnvelop<ComponentDTO>, IUnitOfWork, Principal, IFCMRepositoryContainer> applicationOperationContext)
        {
            var entity = this.mapper.Map<ComponentDTO, Component>(applicationOperationContext.InputDTO.Data);
            var entityIdDb = applicationOperationContext.RepositoryContainer.ComponentRepository.GetByName(entity.Name);
            if(entityIdDb != null)
            {
                applicationOperationContext.RepositoryContainer.ComponentRepository.DeleteById(entityIdDb.AppId);
            }

            if(!string.IsNullOrWhiteSpace(entity.Owner))
            {
                var ownerSystem = applicationOperationContext.RepositoryContainer.ExternalSystemRepository.GetByName(entity.Owner);
                if(ownerSystem == null)
                {
                    throw new ApplicationOperationException(Error.InvalidOwner);
                }

                if(entity.Properties != null && entity.Properties.Any( p => p.Owner != entity.Owner))
                {
                    throw new ApplicationOperationException(Error.InvalidPropertyOwner);
                }
            }
            else if (entity.Properties != null)
            {
                var distinctOwnerNames = entity.Properties.Where(p => !string.IsNullOrWhiteSpace(p.Owner)).Select(f => f.Owner).Distinct();

                foreach (var propertyOwnerName in distinctOwnerNames)
                {
                    var ownerSystem = applicationOperationContext.RepositoryContainer.ExternalSystemRepository.GetByName(propertyOwnerName);
                    if (ownerSystem == null)
                    {
                        throw new ApplicationOperationException(Error.InvalidPropertyOwner);
                    }
                }
            }

            applicationOperationContext.RepositoryContainer.ComponentRepository.Add(entity);

            IEnumerable<ExternalSystem> systemsToNotify = systemsToNotify = applicationOperationContext.RepositoryContainer.ExternalSystemRepository.GetAll();

            this.notificationService.NotifySystemsThatComponentChanged(systemsToNotify, entity);

            return entity.AppId;
        }
    }
}
