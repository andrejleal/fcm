using Framework.Application.Contracts;
using Framework.DomainModel.Repositories;
using FCM.DomainModel.Repositories;
using Framework.Application;
using FCM.DomainModel.Entities;
using AutoMapper;

namespace FCM.Application.Operations
{
    public class RemoveComponentOperation : FCMSysAdminApplicationOperation<string, VoidDTO, IUnitOfWork>
    {
        public RemoveComponentOperation(IMapper mapper, ApplicationOperationDependecyContext<IUnitOfWork, IFCMRepositoryContainer, IFCMRepositoryContainerFactory> dependecyContext) : base(mapper, dependecyContext)
        {
        }

        protected override VoidDTO Run(ApplicationOperationContext<InputEnvelop<string>, IUnitOfWork, Principal, IFCMRepositoryContainer> applicationOperationContext)
        {
            var entity = applicationOperationContext.RepositoryContainer.ComponentRepository.GetByName(applicationOperationContext.InputDTO.Data);
            applicationOperationContext.RepositoryContainer.ComponentRepository.DeleteById(entity.AppId);
            return new VoidDTO();
        }
    }
}
