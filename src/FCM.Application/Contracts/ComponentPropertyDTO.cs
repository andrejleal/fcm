using Framework.Application.Contracts;

namespace FCM.Application.Contracts
{
    public class ComponentPropertyDTO : ExternalEntityDTO
    {
        public string Value
        {
            get;
            set;
        }

        public string Owner
        {
            get;
            set;
        }
    }
}
