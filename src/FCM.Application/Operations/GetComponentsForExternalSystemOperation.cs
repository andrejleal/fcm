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
    public class GetComponentsForExternalSystemOperation : FCMApplicationOperation<VoidDTO, IEnumerable<ComponentDTO>, IUnitOfWork>
    {
        public GetComponentsForExternalSystemOperation(IMapper mapper, ApplicationOperationDependecyContext<IUnitOfWork, IFCMRepositoryContainer, IFCMRepositoryContainerFactory> dependecyContext) : base(mapper, dependecyContext)
        {
        }

        protected override IEnumerable<ComponentDTO> Run(ApplicationOperationContext<InputEnvelop<VoidDTO>, IUnitOfWork, Principal, IFCMRepositoryContainer> applicationOperationContext)
        {
            var authenticatedApplicationName = applicationOperationContext.IdentityId.Application.Name;
            var components = applicationOperationContext.RepositoryContainer.ComponentRepository.GetAllWithProperties().ToList();
            components.RemoveAll(p => p.Owner != null && p.Owner != authenticatedApplicationName);

            foreach(var component in components)
            {
                if (component.Properties != null)
                {
                    var componentProperties = component.Properties.ToList();
                    componentProperties.RemoveAll(p => p.Owner != null && p.Owner != authenticatedApplicationName);

                    var defaultProperties = componentProperties.Where(p => p.Owner == null);

                    foreach(var defaultProperty in defaultProperties)
                    {
                        if(componentProperties.Any(p => p.Name == defaultProperty.Name && p.Owner == authenticatedApplicationName))
                        {
                            componentProperties.Remove(defaultProperty);
                        }
                    }

                    component.Properties = componentProperties;
                }
            }

            return this.mapper.Map<IEnumerable<Component>, IEnumerable<ComponentDTO>>(components);
        }
    }
}
