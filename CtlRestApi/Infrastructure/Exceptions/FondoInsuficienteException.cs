using System;

namespace CtlRestApi.Infrastructure.Exceptions
{
    public class FondoInsuficienteException: Exception
    {
        public FondoInsuficienteException()
        {
        }

        public FondoInsuficienteException(string message)
            : base(message)
        {
        }

        public FondoInsuficienteException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
