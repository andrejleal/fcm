using Framework.Application.Contracts;
using Framework.DomainModel.Entities;
using System;
using System.Collections.Generic;

namespace Framework.Application.Services.CRUDOperations
{
    public interface ICRUDService<TEntity,TEntityDTO, TOutputDTO> 
        where TEntity : DomainEntity
        where TEntityDTO : ExternalEntityDTO
        where TOutputDTO : ExternalEntityDTO
    {
        OutputEnvelop<Guid> Add(InputEnvelop<TEntityDTO> inputData);
        OutputEnvelop Update(InputEnvelop<TEntityDTO> inputData);
        OutputEnvelop Remove(InputEnvelop<Guid> inputData);
        OutputEnvelop<TOutputDTO> Get(InputEnvelop<Guid> inputData);
        OutputEnvelop<IEnumerable<TOutputDTO>> GetAll(InputEnvelop inputData);
    }
}
