using System;
using AutoMapper;
using FCM.Application.Operations;
using FCM.DomainModel.Repositories;
using Framework.Application;
using Framework.DomainModel.Repositories;
using Framework.Infrastruture;
using Framework.Application.Contracts;

namespace FCM.Application
{
    public class FCMApplication : IFCMApplication
    {
        public AddOrUpdateComponentOperation AddOrUpdateComponentOperation { get; private set; }
        public AddOrUpdateExternalSystemOperation AddOrUpdateExternalSystemOperation { get; private set; }
        public GetAllComponentOperation GetAllComponentOperation { get; private set; }
        public GetComponentsForExternalSystemOperation GetComponentsForExternalSystemOperation { get; private set; }
        public GetAllExternalSystemOperation GetAllExternalSystemOperation { get; private set; }
        public RemoveComponentOperation RemoveComponentOperation { get; private set; }
        public RemoveExternalSystemOperation RemoveExternalSystemOperation { get; private set; }
        private StartApplicationOperation startAppApplicationOperation;

        public FCMApplication(IUnitOfWorkFactory<IUnitOfWork> unitOfWorkFactory, IFCMRepositoryContainerFactory repositoryContainerFactory, IMapper mapper, IAuthenticationProvider authenticationProvider, INotificationService notificationService)
        {
            var dependecyContext = new ApplicationOperationDependecyContext<IUnitOfWork, IFCMRepositoryContainer, IFCMRepositoryContainerFactory>();
            dependecyContext.RepositoryContainerFactory = repositoryContainerFactory;
            dependecyContext.UnitOfWorkFactory = unitOfWorkFactory;
            dependecyContext.AuthenticationProvider = authenticationProvider;
            this.AddOrUpdateComponentOperation = new AddOrUpdateComponentOperation(notificationService, mapper, dependecyContext);
            this.AddOrUpdateExternalSystemOperation = new AddOrUpdateExternalSystemOperation(mapper, dependecyContext);
            this.GetAllComponentOperation = new GetAllComponentOperation(mapper, dependecyContext);
            this.GetComponentsForExternalSystemOperation = new GetComponentsForExternalSystemOperation(mapper, dependecyContext);
            this.GetAllExternalSystemOperation = new GetAllExternalSystemOperation(mapper, dependecyContext);
            this.RemoveComponentOperation = new RemoveComponentOperation(mapper, dependecyContext);
            this.RemoveExternalSystemOperation = new RemoveExternalSystemOperation(mapper, dependecyContext);

            this.startAppApplicationOperation = new StartApplicationOperation(mapper, dependecyContext);

            //Initialize application
            this.startAppApplicationOperation.Execute(new InputEnvelop<VoidDTO>(string.Empty, string.Empty, string.Empty, new VoidDTO()));
        }
    }
}
