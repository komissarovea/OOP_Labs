/// Комиссаров Евгений Александрович, 50326-2, вариант 5
using System; // подключение общей библиотеки классов

namespace Samost // пространство имён
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
