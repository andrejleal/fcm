
using System;

namespace Framework.Application.Contracts
{
    public class InputEnvelop
    {
        public string ApplicationRequestId {
            get;
            private set;
        }

        public string CorrelationId
        {
            get;
            private set;
        }

        public string AuthenticationToken
        {
            get;
            private set;
        }

        public InputEnvelop(string requestId, string correlationId, string authenticationToken)
        {
            this.ApplicationRequestId = requestId;
            this.CorrelationId = correlationId;
            this.AuthenticationToken = authenticationToken;
        }
    }

    public class InputEnvelop<T> : InputEnvelop
    {
        public T Data { get; private set; }

        public InputEnvelop(string requestId, string correlationId, string authenticationToken, T data) : base(requestId, correlationId, authenticationToken)
        {
            this.Data = data;
        }
    }
}
