namespace Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    public class UserNotCheckedInException : Exception
    {
        public UserNotCheckedInException()
        {
        }

        public UserNotCheckedInException(string message) 
            : base(message)
        {
        }

        public UserNotCheckedInException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected UserNotCheckedInException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
