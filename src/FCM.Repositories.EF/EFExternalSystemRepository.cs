using System;
using FCM.DomainModel.Entities;
using FCM.DomainModel.Repositories;
using Framework.Repositories.EF;
using System.Linq;

namespace FCM.Repositories.EF
{
    public class EFExternalSystemRepository : EFAggregateRootRepository<ExternalSystem>, IExternalSystemRepository
    {
        public EFExternalSystemRepository(FCMContext context) : base(context)
        {
        }

        public ExternalSystem GetByName(string externalSystemName)
        {
            return this.context.Set<ExternalSystem>().FirstOrDefault(e => e.Name == externalSystemName);
        }

        public ExternalSystem GetByToken(string token)
        {
            return this.context.Set<ExternalSystem>().FirstOrDefault(e => e.Token == token || e.AlternateToken == token);
        }
    }
}
