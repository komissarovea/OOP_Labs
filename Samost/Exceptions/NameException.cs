/// Комиссаров Евгений Александрович, 50326-2, вариант 5
using System;

namespace Samost
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
