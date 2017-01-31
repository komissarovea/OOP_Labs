/// Комиссаров Евгений Александрович, 50326-2, вариант 5
using System; // подключение общей библиотеки классов

namespace Samost // пространство имён
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
