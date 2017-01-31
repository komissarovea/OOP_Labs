/// Комиссаров Евгений Александрович, 50326-2, вариант 5
using System; // подключение общей библиотеки классов
using System.Collections.Generic; // подключение библиотеки классов, определяющих типизированные коллекции

namespace Samost // пространство имён
{
    /// <summary>
    /// Клиент банка (вкладчик)
    /// </summary>
    public class Client
    {
        #region Fields

        private string _name; // поле для имени клиента
        private List<Deposit> _deposits = new List<Deposit>(); // поле для списка вкладов

        #endregion

        #region Properties

        /// <summary>
        /// Имя клиента (ФИО)
        /// </summary>
        public string Name
        {
            get { return _name; } // вернуть значение поля _name
            set
            {
                if (String.IsNullOrEmpty(value) || value.Length <= 2) // проверка нового значения на количество символов
                    throw new NameException("Ошибка! Имя вкладчика должно быть длиннее двух букв!"); // сгенерировать ошибку NameException
                else // если больше 2
                    _name = value; // присвоить новое значение 
            }
        }

        /// <summary>
        /// Список вкладов
        /// </summary>
        public List<Deposit> Deposits { get { return _deposits; } } // вернуть значение поля _deposits

        /// <summary>
        /// Бонус в процентах от вклада
        /// </summary>
        public int PercentBonus { get; set; }  // задать или считать значение

        /// <summary>
        /// Бонус в виде фиксированной суммы
        /// </summary>
        public int FixBonus { get; set; } // задать или считать значение

        #endregion

        #region Methods

        /// <summary>
        /// Посчитать сумму выплат для данного клиента
        /// </summary>
        /// <returns></returns>
        public int GetPayoutSum()
        {
            int clientSum = 0; // инициализация переменной clientSum
            foreach (var deposit in this.Deposits) // перебор вкладов клиента
            {
                clientSum += deposit.GetPayoutSum(); // посчитать сумму выплат по вкладу и добавить к общей сумме
                clientSum += this.FixBonus; // добавить бонус в виде фиксированной суммы
                clientSum += deposit.Sum * this.PercentBonus / 100; // добавить бонус в процентах от вклада
            }
            return clientSum; // вернуть clientSum
        }

        /// <summary>
        /// Вернуть строку, которая описывает данный объект
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0}, бонус в процентах - {1}%, фиксированный бонус - {2}",
                this.Name, this.PercentBonus, this.FixBonus); // вернуть форматированную строку
        }

        #endregion
    }
}
