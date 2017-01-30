using System;

namespace Samost.Exceptions
{
    public class NameException : Exception
    {
        public NameException(string message) : base(message)
        {
        }
    }
}
