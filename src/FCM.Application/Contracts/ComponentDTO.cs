using Framework.Application.Contracts;
using System.Collections.Generic;

namespace FCM.Application.Contracts
{
    public class ComponentDTO : ExternalEntityDTO
    {
        public string Owner
        {
            get;
            set;
        }

        public IEnumerable<ComponentPropertyDTO> Properties
        {
            get;
            set;
        }
    }
}
