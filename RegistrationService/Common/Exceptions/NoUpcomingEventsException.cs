namespace Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    public class NoUpcomingEventsException : Exception
    {
        public NoUpcomingEventsException()
        {
        }

        public NoUpcomingEventsException(string message) 
            : base(message)
        {
        }

        public NoUpcomingEventsException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected NoUpcomingEventsException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
