using System;

namespace CtlRestApi.Infrastructure.Exceptions
{
    public class CuentasDiferentesException: Exception
    {
        public CuentasDiferentesException()
        {
        }

        public CuentasDiferentesException(string message)
            : base(message)
        {
        }

        public CuentasDiferentesException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
