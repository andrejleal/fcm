namespace FCM.API.REST.Contracts
{
    public class ExternalSystemModel
    {
        public string Name
        {
            get;
            set;
        }
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
