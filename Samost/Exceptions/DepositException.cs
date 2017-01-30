using System;

namespace Samost.Exceptions
{
    /// <summary>
    /// Ошибка пополнения вклада
    /// </summary>
    public class DepositException : Exception
    {
        /// <summary>
        /// Конструктор, задающий текст ошибки
        /// </summary>
        /// <param name="message"></param>
        public DepositException(string message) : base(message)
        {
        }
    }
}
