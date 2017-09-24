using Framework.Application.Contracts;

namespace FCM.Application.Contracts
{
    public class ExternalSystemDTO : ExternalEntityDTO
    {
        public string Token
        {
            get;
            set;
        }
        public string AlternateToken
        {
            get;
            set;
        }

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
        public string NotificationToken
        {
            get;
            set;
        }
    }
}
