namespace FCM.DomainModel
{
    public class Error : Framework.DomainModel.Error
    {
        public static Error InvalidOwner = new Error("InvalidOwner");
        public static Error InvalidPropertyOwner = new Error("InvalidPropertyOwner");
        
        public Error(string code) : base (code)
        {

        }
    }
}
