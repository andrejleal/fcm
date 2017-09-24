using FCM.DomainModel.Entities;
using Framework.DomainModel.Repositories;
using FCM.DomainModel.Repositories;
using Framework.Application;
using Framework.Application.Contracts;
using FCM.Application.Contracts;
using System.Collections.Generic;
using AutoMapper;

namespace FCM.Application.Operations
{
    public class GetAllExternalSystemOperation : FCMSysAdminApplicationOperation<VoidDTO, IEnumerable<ExternalSystemOutputDTO>, IUnitOfWork>
    {
        public GetAllExternalSystemOperation(IMapper mapper, ApplicationOperationDependecyContext<IUnitOfWork, IFCMRepositoryContainer, IFCMRepositoryContainerFactory> dependecyContext) : base(mapper, dependecyContext)
        {
        }

        protected override IEnumerable<ExternalSystemOutputDTO> Run(ApplicationOperationContext<InputEnvelop<VoidDTO>, IUnitOfWork, Principal, IFCMRepositoryContainer> applicationOperationContext)
        {
            var entities = applicationOperationContext.RepositoryContainer.ExternalSystemRepository.GetAll();
            return this.mapper.Map<IEnumerable<ExternalSystem>, IEnumerable<ExternalSystemOutputDTO>>(entities);
        }
    }
}
