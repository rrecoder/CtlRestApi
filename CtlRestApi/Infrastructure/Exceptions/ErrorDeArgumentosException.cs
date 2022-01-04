using System;

namespace CtlRestApi.Infrastructure.Exceptions
{
    public class ErrorDeArgumentosException: Exception
    {
        public ErrorDeArgumentosException()
        {
        }

        public ErrorDeArgumentosException(string message)
            : base(message)
        {
        }

        public ErrorDeArgumentosException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
