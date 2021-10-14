using System;

namespace PlumsailTest.Infrastructure.Exceptions
{
    public class AppBadRequestException : AppException
    {
        private readonly string _field;

        public string Field => _field ?? Guid.NewGuid().ToString();

        public string ErrorMessage { get; }
        
        public AppBadRequestException() : this(null, null)
        {
        }

        public AppBadRequestException(string message) : this(null, message)
        {
        }

        public AppBadRequestException(string field, string message) : base(message)
        {
            _field = field;
            ErrorMessage = message;
        }
    }
}