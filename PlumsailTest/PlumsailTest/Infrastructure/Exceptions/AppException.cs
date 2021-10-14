using System;

namespace PlumsailTest.Infrastructure.Exceptions
{
    public class AppException: ApplicationException
    {
        public AppException()
        {
        }

        public AppException(string message) : base(message)
        {
        }

        public AppException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}