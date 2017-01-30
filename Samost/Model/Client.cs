/// Комиссаров Евгений Александрович, 50326-2, вариант 5
using System;
using System.Collections.Generic;

namespace Samost
{
    /// <summary>
    /// Клиент банка (вкладчик)
    /// </summary>
    public class Client
    {
        #region Fields

        private List<Deposit> _deposits = new List<Deposit>();
        private string _name;

        #endregion

        #region Properties

        /// <summary>
        /// Имя клиента (ФИО)
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (String.IsNullOrEmpty(value) || value.Length <= 2)
                    throw new NameException("Ошибка! Имя вкладчика должно быть длиннее двух букв!");
                else
                    _name = value;
            }
        }

        /// <summary>
        /// Список вкладов
        /// </summary>
        public List<Deposit> Deposits { get { return _deposits; } }

        /// <summary>
        /// Бонус в процентах от вклада
        /// </summary>
        public int PercentBonus { get; set; }

        /// <summary>
        /// Бонус в виде фиксированной суммы
        /// </summary>
        public int FixBonus { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Посчитать сумму выплат для данного клиента
        /// </summary>
        /// <returns></returns>
        public int GetPayoutSum()
        {
            int clientSum = 0;
            foreach (var deposit in this.Deposits)
            {
                clientSum += deposit.GetPayoutSum();
                clientSum += this.FixBonus;
                clientSum += deposit.Sum * this.PercentBonus / 100;
            }
            return clientSum;
        }

        /// <summary>
        /// Возвращает строку, которая описывает данный объект
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0}, бонус в процентах - {1}%, фиксированный бонус - {2}",
                this.Name, this.PercentBonus, this.FixBonus);
        }

        #endregion
    }
}
