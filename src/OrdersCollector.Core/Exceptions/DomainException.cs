using System;

namespace OrdersCollector.Core.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public string ErrorCode { get; }
    }
}