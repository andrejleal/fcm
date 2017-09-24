using FCM.DomainModel.Entities;
using FCM.DomainModel.Repositories;
using Framework.DomainModel.Exceptions;
using Framework.DomainModel.Repositories;
using System;
using System.Security.Principal;

namespace Framework.Infrastruture
{
    public class FCMAuthenticationProvider : IAuthenticationProvider
    {
        private IFCMRepositoryContainerFactory repositoryContainerFactory;
        private IUnitOfWorkFactory<IUnitOfWork> unitOfWorkFactory;

        public FCMAuthenticationProvider(IFCMRepositoryContainerFactory repositoryContainerFactory, IUnitOfWorkFactory<IUnitOfWork> unitOfWorkFactory)
        {
            this.repositoryContainerFactory = repositoryContainerFactory;
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public IPrincipal Authenticate(string token)
        {
            using (var unitOfWork = this.unitOfWorkFactory.Create())
            {
                var repoContainer = this.repositoryContainerFactory.GetInstance(unitOfWork);
                var entity = repoContainer.ExternalSystemRepository.GetByToken(this.GetSecuredToken(token));
                if(entity == null)
                {
                    throw new AuthenticationException();
                }
                return new Principal(entity);
            }            
        }

        public string GetSecuredToken(string token)
        {
            char[] charArray = token.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
