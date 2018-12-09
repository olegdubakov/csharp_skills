namespace Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    public class UserIsAlreadyCheckedInException : Exception
    {
        public UserIsAlreadyCheckedInException()
        {
        }

        public UserIsAlreadyCheckedInException(string message) 
            : base(message)
        {
        }

        public UserIsAlreadyCheckedInException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected UserIsAlreadyCheckedInException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
