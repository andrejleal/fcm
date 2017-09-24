using FCM.DomainModel.Entities;
using Framework.DomainModel.Repositories;

namespace FCM.DomainModel.Repositories
{
    public interface IExternalSystemRepository : IAggregateRootRepository<ExternalSystem>
    {
        ExternalSystem GetByName(string externalSystemName);
        ExternalSystem GetByToken(string externalSystemName);
    }
}
