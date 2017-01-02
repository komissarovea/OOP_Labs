using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samost.Exceptions
{
    public class NameException : Exception
    {
        public NameException(string message) : base(message)
        {
        }
    }
}
