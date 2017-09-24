
using Framework.Application.Contracts;
using Framework.DomainModel;
using Framework.DomainModel.Exceptions;
using Framework.DomainModel.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Principal;

namespace Framework.Application
{
    public abstract class ApplicationOperation<TInputEnvelop, TInput, TOutputEnvelop, TOutput, TUnitOfWork, TPrincipalEntity, TRepositoryContainer, TRepositoryContainerFactory>
        where TInputEnvelop : InputEnvelop<TInput>
        where TOutputEnvelop : OutputEnvelop<TOutput>, new()
        where TUnitOfWork : IUnitOfWork
        where TPrincipalEntity : IPrincipal
        where TRepositoryContainer : IRepositoryContainer
        where TRepositoryContainerFactory: IRepositoryContainerFactory<TRepositoryContainer>
    {

        public virtual bool RequiresAuthentication
        {
            get
            {
                return true;
            }
        }

        public virtual IsolationLevel IsolationLevel
        {
            get
            {
                return IsolationLevel.ReadCommitted;
            }
        }

        public ApplicationOperationDependecyContext<TUnitOfWork,TRepositoryContainer, TRepositoryContainerFactory> DependecyContext
        {
            get;
            private set;
        }

        public ApplicationOperation(ApplicationOperationDependecyContext<TUnitOfWork, TRepositoryContainer, TRepositoryContainerFactory> dependecyContext)
        {
            this.DependecyContext = dependecyContext;
        }

        protected abstract TPrincipalEntity Authenticate(ApplicationOperationContext<TInputEnvelop, TUnitOfWork, TPrincipalEntity, TRepositoryContainer> applicationOperationContext);

        protected abstract bool Authorize(ApplicationOperationContext<TInputEnvelop, TUnitOfWork, TPrincipalEntity, TRepositoryContainer> applicationOperationContext);

        protected abstract TOutput Run(ApplicationOperationContext<TInputEnvelop, TUnitOfWork, TPrincipalEntity, TRepositoryContainer> applicationOperationContext);

        public TOutputEnvelop Execute(TInputEnvelop inputDTO)
        {
            try
            {
                var outputEnvelop = new TOutputEnvelop();

                using (TUnitOfWork unitOfWork = this.DependecyContext.UnitOfWorkFactory.Create(this.IsolationLevel))
                {
                    var repositoryContainer = this.DependecyContext.RepositoryContainerFactory.GetInstance(unitOfWork);
                    var applicationOperationContext = new ApplicationOperationContext<TInputEnvelop, TUnitOfWork, TPrincipalEntity, TRepositoryContainer>(inputDTO, unitOfWork, repositoryContainer);

                    if (this.RequiresAuthentication)
                    {
                        applicationOperationContext.IdentityId = this.Authenticate(applicationOperationContext);

                        if (applicationOperationContext.IdentityId == null)
                        {
                            throw new ApplicationOperationException(Error.AuthenticationError);
                        }

                        if (!this.Authorize(applicationOperationContext))
                        {
                            throw new ApplicationOperationException(Error.AuthorizationError);
                        }
                    }

                    outputEnvelop.ApplicationRequestId = applicationOperationContext.InputDTO.ApplicationRequestId;
                    outputEnvelop.Data = this.Run(applicationOperationContext);
                    applicationOperationContext.UnitOfWork.Commit();
                }
                return outputEnvelop;
            }
            catch (ApplicationOperationException ex)
            {
                Console.WriteLine(ex);
                return this.GetOutputEnvelop(inputDTO, ex.Error.Code);
            }
            catch (AuthenticationException ex)
            {
                Console.WriteLine(ex);
                return this.GetOutputEnvelop(inputDTO, Error.AuthenticationError.Code);
            }
            catch (AuthorizationException ex)
            {
                Console.WriteLine(ex);
                return this.GetOutputEnvelop(inputDTO, Error.AuthorizationError.Code);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return this.GetOutputEnvelop(inputDTO, Error.Unexpected.Code );
            }
        }

        private TOutputEnvelop GetOutputEnvelop(TInputEnvelop inputDTO, string errorCode)
        {
            return this.GetOutputEnvelop(inputDTO, new List<string>() { errorCode });
        }
        private TOutputEnvelop GetOutputEnvelop(TInputEnvelop inputDTO, IEnumerable<string> errorCodes)
        {
            var outputEnvelop = new TOutputEnvelop();
            outputEnvelop.ApplicationRequestId = inputDTO.ApplicationRequestId;
            outputEnvelop.Errors = errorCodes; 
            return outputEnvelop;
        }
    }
}
