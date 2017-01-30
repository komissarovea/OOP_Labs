using Samost.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Samost
{
    /// <summary>
    /// Дата начала срока
    /// Периодичность капитализации
    /// </summary>
    public class Deposit
    {
        #region Fields

        const int MIN_PAYMENT = 100;

        private List<Payment> _payments = new List<Payment>();

        #endregion

        #region  Properties

        /// <summary>
        /// Тип вклада
        /// </summary>
        public DepositType Type { get; set; }

        /// <summary>
        /// Список платежей по вкладу
        /// </summary>
        public List<Payment> Payments
        {
            get { return _payments; }
        }

        /// <summary>
        /// Общая сумма вклада
        /// </summary>
        public int Sum
        {
            get { return _payments.Sum(p => p.Sum); }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Перегрузка оператора "++" для пополнения вклада на фиксированную сумму(100)
        /// </summary>
        /// <param name="deposit"></param>
        /// <returns></returns>
        public static Deposit operator ++(Deposit deposit)
        {
            return deposit.Replenish(MIN_PAYMENT);
        }

        /// <summary>
        /// Перегрузка оператора "+" для пополнения вклада на заданную сумму
        /// </summary>
        /// <param name="deposit"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        public static Deposit operator +(Deposit deposit, int payment)
        {
            return deposit.Replenish(payment);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Пополнить вклад на заданную сумму
        /// </summary>
        /// <param name="paymentSum"></param>
        /// <returns></returns>
        public Deposit Replenish(int paymentSum)
        {
            if (this.Sum + paymentSum < 0)
                throw new DepositException("Ошибка! Величина вклада не может быть отрицательной!");
            Payment payment = new Payment() { Sum = paymentSum, Date = DateTime.Now };
            this.Payments.Add(payment);

            return this;
        }

        /// <summary>
        /// Возращает сумму выплат по вкладу
        /// </summary>
        /// <returns></returns>
        public int GetPayoutSum()
        {
            int payout = 0;
            if (this.Payments.Count > 0)
            {
                int tempSum = 0;
                List<Payment> sortedPayments = this.Payments.OrderBy(p => p.Date).ToList();
                DateTime initialDate = sortedPayments.First().Date;
                int daysInYear = GetDaysInYear(initialDate);
                double dayPercent = (double)this.Type.Percent / daysInYear;
                int count = this.Payments.Count;
                for (int i = 0; i < count; i++)
                {
                    tempSum += sortedPayments[i].Sum;
                    DateTime nextDate = i + 1 == count ? DateTime.Now : sortedPayments[i + 1].Date;
                    int days = (nextDate - sortedPayments[i].Date).Days;
                    payout += (int)(days * dayPercent * tempSum / 100);
                }
            }
            return payout;
        }

        /// <summary>
        /// Возвращает количество дней в году для заданной даты
        /// </summary>
        /// <param name="thisDate"></param>
        /// <returns></returns>
        private int GetDaysInYear(DateTime thisDate)
        {
            var nextDate = thisDate.AddYears(1);
            return (nextDate - thisDate).Days;
        }

        /// <summary>
        /// Возвращает строку, которая описывает данный объект
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0}, сумма - {1} руб.", Type.Name, this.Sum);
        }

        #endregion
    }
}
