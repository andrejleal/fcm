using Framework.DomainModel.Entities;
using System.Collections.Generic;

namespace FCM.DomainModel.Entities
{
    public class Component : DomainEntity
    {
        public string Name
        {
            get;
            set;
        }

        public string Owner
        {
            get;
            set;
        }

        public IEnumerable<ComponentProperty> Properties
        {
            get;
            set;
        }
    }
}
