using System;

namespace Samost.Exceptions
{
    /// <summary>
    /// Ошибка именования
    /// </summary>
    public class NameException : Exception
    {
        /// <summary>
        /// Конструктор, задающий текст ошибки
        /// </summary>
        /// <param name="message"></param>
        public NameException(string message) : base(message)
        {
        }
    }
}
