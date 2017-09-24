using FCM.DomainModel.Entities;
using Framework.Application.Contracts;
using Framework.DomainModel.Repositories;
using System.Collections.Generic;
using FCM.DomainModel.Repositories;
using Framework.Application;
using FCM.Application.Contracts;
using AutoMapper;
using System.Linq;

namespace FCM.Application.Operations
{
    public class GetAllComponentOperation : FCMSysAdminApplicationOperation<VoidDTO, IEnumerable<ComponentDTO>, IUnitOfWork>
    {
        public GetAllComponentOperation(IMapper mapper, ApplicationOperationDependecyContext<IUnitOfWork, IFCMRepositoryContainer, IFCMRepositoryContainerFactory> dependecyContext) : base(mapper, dependecyContext)
        {
        }

        protected override IEnumerable<ComponentDTO> Run(ApplicationOperationContext<InputEnvelop<VoidDTO>, IUnitOfWork, Principal, IFCMRepositoryContainer> applicationOperationContext)
        {
            var components = applicationOperationContext.RepositoryContainer.ComponentRepository.GetAllWithProperties().ToList();
            return this.mapper.Map<IEnumerable<Component>, IEnumerable<ComponentDTO>>(components);
        }
    }
}
