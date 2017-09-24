using Framework.DomainModel.Repositories;
using AutoMapper;
using FCM.DomainModel.Repositories;
using Framework.Application;
using FCM.DomainModel.Entities;
using Framework.Application.Contracts;
using System.Linq;
using System.Collections.Generic;

namespace FCM.Application.Operations
{
    public class StartApplicationOperation : FCMApplicationOperation<VoidDTO, VoidDTO, IUnitOfWork>
    {
        public StartApplicationOperation(IMapper mapper, ApplicationOperationDependecyContext<IUnitOfWork, IFCMRepositoryContainer, IFCMRepositoryContainerFactory> dependecyContext) : base(mapper, dependecyContext)
        {
        }

        public override bool RequiresAuthentication
        {
            get
            {
                return false;
            }
        }

        protected override VoidDTO Run(ApplicationOperationContext<InputEnvelop<VoidDTO>, IUnitOfWork, Principal, IFCMRepositoryContainer> applicationOperationContext)
        {

            if (!applicationOperationContext.RepositoryContainer.ExternalSystemRepository.GetAll().Any())
            {
                var externalSystemSysAdmin = new ExternalSystem()
                {
                    Name = "sysadmin",
                    Token = this.DependecyContext.AuthenticationProvider.GetSecuredToken("sysadmin"),
                    AlternateToken = this.DependecyContext.AuthenticationProvider.GetSecuredToken("sa"),
                    IsSysAdmin = true
                };

                applicationOperationContext.RepositoryContainer.ExternalSystemRepository.Add(externalSystemSysAdmin);
                var nrOfClientApps = 3;

                for (int i = 1; i <= nrOfClientApps + 1; ++i)
                {
                    var clientAppName = $"clientApp{i}";
                    var clientAppToken = $"clientAppPw{i}";
                    var clientAppNotificationToken = $"clientAppNotificationToken{i}";
                    var externalSystemClientApp = new ExternalSystem()
                    {
                        Name = clientAppName,
                        Token = this.DependecyContext.AuthenticationProvider.GetSecuredToken(clientAppToken),
                        AlternateToken = this.DependecyContext.AuthenticationProvider.GetSecuredToken(clientAppToken),
                        NotificationToken = clientAppNotificationToken,
                        NotificationURL = $"http://localhost:5002/api/externalsystems/{clientAppName}/components/refresh"
                    };

                    applicationOperationContext.RepositoryContainer.ExternalSystemRepository.Add(externalSystemClientApp);
                }

                var component = new Component();
                component.Name = "defaultToggle";
                component.Properties = new List<ComponentProperty>
                {
                    new ComponentProperty()
                    {
                        Name = "value",
                        Value = "true"
                    },
                    new ComponentProperty()
                    {
                        Name = "width",
                        Value = "100px"
                    }
                };

                applicationOperationContext.RepositoryContainer.ComponentRepository.Add(component);

                component = new Component();
                component.Name = "defaultToggle2";
                var componentProperties = new List<ComponentProperty>();
                for (int i = 1; i <= nrOfClientApps; ++i)
                {
                    componentProperties.Add(new ComponentProperty()
                    {
                        Name = "value",
                        Value = i % 2 == 0 ? "true" : "false",
                        Owner = $"clientApp{i}"
                });
                }

                component.Properties = componentProperties;

                applicationOperationContext.RepositoryContainer.ComponentRepository.Add(component);

                for (int i = 1; i <= nrOfClientApps; ++i)
                {
                    component = new Component();
                    component.Name = $"toggleClient{i}";
                    component.Owner = $"clientApp{i}";
                    component.Properties = new List<ComponentProperty>
                    {
                        new ComponentProperty()
                        {
                            Name = "value",
                            Value = i%2==0 ? "true" : "false"
                        }
                    };

                    applicationOperationContext.RepositoryContainer.ComponentRepository.Add(component);
                }


            }

            return new VoidDTO();
        }
    }
}
