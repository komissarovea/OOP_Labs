/// Комиссаров Евгений Александрович, 50326-2, вариант 5
using System;

namespace Samost
{
    /// <summary>
    /// Тип вклада (хранит описание процентов)
    /// </summary>
    public class DepositType
    {
        #region Properties

        /// <summary>
        /// Ключ (идентификатор типа вклада)
        /// </summary>
        public int Key { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Процент
        /// </summary>
        public int Percent { get; set; }

        #endregion

        #region Methods
        
        /// <summary>
        /// Вернуть строку, которая описывает данный объект
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0} - {1}%", this.Name ?? "", this.Percent);
        } 

        #endregion
    }
}
