using Framework.DomainModel.Repositories;
using Framework.Infrastruture;

namespace Framework.Application
{
    public class ApplicationOperationDependecyContext<TUnitOfWork, TRepositoryContainer, TRepositoryContainerFactory>
        where TUnitOfWork: IUnitOfWork
        where TRepositoryContainer : IRepositoryContainer
        where TRepositoryContainerFactory: IRepositoryContainerFactory<TRepositoryContainer>
    {
        public IUnitOfWorkFactory<TUnitOfWork> UnitOfWorkFactory {
            get;
            set;
        }

        public TRepositoryContainerFactory RepositoryContainerFactory
        {
            get;
            set;
        }

        public IAuthenticationProvider AuthenticationProvider
        {
            get;
            set;
        }
    }
}
