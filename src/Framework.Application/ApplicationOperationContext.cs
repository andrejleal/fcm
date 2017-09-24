using Framework.Application.Contracts;
using Framework.DomainModel.Entities;
using Framework.DomainModel.Repositories;
using System.Security.Principal;

namespace Framework.Application
{
    public class ApplicationOperationContext<TInputDTO, TUnitOfWork, TAuthenticatedEntity, TRepositoryContainer>
        where TInputDTO : InputEnvelop
        where TUnitOfWork : IUnitOfWork
        where TAuthenticatedEntity : IPrincipal
        where TRepositoryContainer: IRepositoryContainer
    {
        public TInputDTO InputDTO
        {
            get;
            private set;
        }

        public TUnitOfWork UnitOfWork
        {
            get;
            private set;
        }

        public TRepositoryContainer RepositoryContainer
        {
            get;
            private set;
        }

        public TAuthenticatedEntity IdentityId
        {
            get;
            internal set;
        }

        public ApplicationOperationContext(TInputDTO inputDto, TUnitOfWork unitOfWork, TRepositoryContainer repositoryContainer)
        {
            this.InputDTO = inputDto;
            this.UnitOfWork = unitOfWork;
            this.RepositoryContainer = repositoryContainer;
        }
    }
}
