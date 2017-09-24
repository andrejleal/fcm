using System;

namespace Framework.DomainModel.Entities
{
    public class DomainEntity
    {
        public int Id
        {
            get;
            protected set;
        }

        public Guid AppId
        {
            get;
            set;
        }

        public Guid ConcurrencyToken
        {
            get;
            set;
        }

        public DateTimeOffset CreationTimestamp
        {
            get;
            set;
        }

        public DateTimeOffset LastUpdateTimestamp
        {
            get;
            set;
        }

        public DomainEntity(Guid appId) : this(appId, Guid.NewGuid())
        {
        }
        public DomainEntity() : this(Guid.NewGuid(), Guid.NewGuid())
        {
        }

        private DomainEntity(Guid appId, Guid concurrencyToken)
        {
            this.AppId = appId;
            this.ConcurrencyToken = concurrencyToken;
        }
    }
}
