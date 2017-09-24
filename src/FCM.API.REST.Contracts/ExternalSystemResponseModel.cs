namespace FCM.API.REST.Contracts
{
    public class ExternalSystemResponseModel
    {
        public string Name
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
    }
}
