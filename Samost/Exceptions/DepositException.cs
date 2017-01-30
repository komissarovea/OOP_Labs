/// Комиссаров Евгений Александрович, 50326-2, вариант 5
using System;

namespace Samost
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
