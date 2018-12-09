namespace Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    public class EventNotExistException : Exception
    {
        public EventNotExistException()
        {
        }

        public EventNotExistException(string message) 
            : base(message)
        {
        }

        public EventNotExistException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected EventNotExistException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
