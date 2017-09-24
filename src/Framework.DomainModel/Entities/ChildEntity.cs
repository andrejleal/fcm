using System;

namespace Framework.DomainModel.Entities
{
    public class ChildEntity<TParent> : DomainEntity
        where TParent : DomainEntity
    {
        private TParent parent;

        public int ParentId
        {
            get;
            protected set;
        }
        public TParent Parent {
            get
            {
                return this.parent;
            }
            set
            {
                this.parent = value;
                this.ParentId = parent.Id;
            }
        }
        public ChildEntity() : base()
        {
            
        }
    }
}
