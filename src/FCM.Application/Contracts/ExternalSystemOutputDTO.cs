using Framework.Application.Contracts;

namespace FCM.Application.Contracts
{
    public class ExternalSystemOutputDTO : ExternalEntityDTO
    {
        public bool IsSysAdmin
        {
            get;
            set;
        }
        public string NotificationURL
        {
            get;
            set;
        }
    }
}
