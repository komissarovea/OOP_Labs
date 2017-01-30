/// Комиссаров Евгений Александрович, 50326-2, вариант 5
using System;

namespace Samost
{
    /// <summary>
    /// Платёж (пополнение вклада)
    /// </summary>
    public class Payment
    {
        #region Properties
        
        /// <summary>
        /// Сумма платежа
        /// </summary>
        public int Sum { get; set; }

        /// <summary>
        /// Дата платежа
        /// </summary>
        public DateTime Date { get; set; } 

        #endregion

        #region Methods

        /// <summary>
        /// Возвращает строку, которая описывает данный объект
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0} - {1} руб.", this.Date, this.Sum);
        }

        #endregion
    }
}
