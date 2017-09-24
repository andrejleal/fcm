using System;

namespace Framework.DomainModel.Exceptions
{
    public class ApplicationOperationException : Exception
    {
        public Error Error { get; private set; }

        public ApplicationOperationException(Error error)
        {
            this.Error = error;
        }

    }
}
