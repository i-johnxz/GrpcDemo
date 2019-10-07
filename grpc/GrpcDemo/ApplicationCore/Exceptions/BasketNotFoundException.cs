using System;
using System.Runtime.Serialization;

namespace ApplicationCore.Exceptions
{
    public class BasketNotFoundException : Exception
    {
        public BasketNotFoundException(int basketId) : base($"No basket found with id {basketId}")
        {
            
        }

        protected BasketNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            
        }

        public BasketNotFoundException(string message) : base(message)
        {
            
        }

        public BasketNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}