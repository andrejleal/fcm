using System.Linq;

namespace FCM.API.REST.Contracts
{
    public class RestResponse
    {
        public bool Success
        {
            get
            {
                return this.Errors == null || !this.Errors.Any();
            }
        }
        public string[] Errors {
            get;
            set;
        }
    }

    public class RestResponseWithData<T> : RestResponse
    {
        public T Data { get; set; }
    }
}
