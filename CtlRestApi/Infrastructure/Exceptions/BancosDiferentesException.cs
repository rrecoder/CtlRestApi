using System;

namespace CtlRestApi.Infrastructure.Exceptions
{ 
    public class BancosDiferentesException : Exception
    {
        public BancosDiferentesException()
        {
        }

        public BancosDiferentesException(string message)
            : base(message)
        {
        }

        public BancosDiferentesException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
