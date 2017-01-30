using System;

namespace Samost.Exceptions
{
    public class DepositException : Exception
    {
        public DepositException(string message) : base(message)
        {
        }
    }
}
