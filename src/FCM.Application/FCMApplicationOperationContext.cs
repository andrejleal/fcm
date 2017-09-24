using FCM.DomainModel.Entities;
using FCM.DomainModel.Repositories;
using Framework.Application.Contracts;
using Framework.DomainModel.Repositories;

namespace Framework.Application
{
    public class FCMApplicationOperationContext<TInputDTO, TUnitOfWork, TAuthenticatedEntity> : ApplicationOperationContext<TInputDTO, TUnitOfWork, Principal, IFCMRepositoryContainer>
        where TInputDTO : InputEnvelop
        where TUnitOfWork : IUnitOfWork
    {
        public string AuthenticationToken
        {
            get;
            private set;
        }

        public FCMApplicationOperationContext(TInputDTO inputDto, TUnitOfWork unitOfWork, IFCMRepositoryContainer repositoryContainer) : base(inputDto, unitOfWork, repositoryContainer)
        {
        }
    }
}
