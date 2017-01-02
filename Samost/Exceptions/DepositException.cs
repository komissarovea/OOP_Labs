using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samost.Exceptions
{
    public class DepositException : Exception
    {
        public DepositException(string message) : base(message)
        {
        }
    }
}
