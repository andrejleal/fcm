namespace Framework.DomainModel
{
    public class Error
    {
        public static Error AuthenticationError = new Error("AuthenticationError");
        public static Error AuthorizationError = new Error("AuthorizationError");
        public static Error Unexpected = new Error("Unexpected");

        protected Error(string code)
        {
            this.Code = code;
        }

        public string Code { get; private set; }
    }
}
