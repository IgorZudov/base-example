using System;
using System.Runtime.Serialization;

namespace Ziv.CodeExample
{
    public class ClientException : Exception
    {
        public ClientException()
        {
        }

        public ClientException(string message) : base(message)
        {
        }

        protected ClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ClientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}