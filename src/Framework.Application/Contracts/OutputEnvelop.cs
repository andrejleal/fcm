
using System.Collections.Generic;

namespace Framework.Application.Contracts
{
    public class OutputEnvelop
    {
        public string ApplicationRequestId
        {
            get;
            set;
        }

        public IEnumerable<string> Errors
        {
            get;
            set;
        }
    }
    public class OutputEnvelop<T> : OutputEnvelop
    {

        public T Data
        {
            get;
            set;
        }
    }
}
