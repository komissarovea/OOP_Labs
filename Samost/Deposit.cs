using Samost.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //private int _sum = 0;
        private List<Payment> _payments = new List<Payment>();

        #endregion

        #region  Properties

        public DepositType Type { get; set; }

        public List<Payment> Payments
        {
            get { return _payments; }
        }

        public int Sum
        {
            get { return _payments.Sum(p => p.Sum); }
            //private set
            //{
            //    if (value < 0)
            //        throw new DepositException("Ошибка! Величина вклада не может быть отрицательной!");
            //    _sum = value;
            //}
        }

        #endregion

        #region Operators

        public static Deposit operator ++(Deposit deposit)
        {
            return deposit.Replenish(MIN_PAYMENT);
        }

        public static Deposit operator +(Deposit deposit, int payment)
        {

            return deposit.Replenish(payment);
        }

        #endregion

        #region Methods

        public Deposit Replenish(int paymentSum)
        {
            if (this.Sum + paymentSum < 0)
                throw new DepositException("Ошибка! Величина вклада не может быть отрицательной!");
            Payment payment = new Payment() { Sum = paymentSum, Date = DateTime.Now };
            this.Payments.Add(payment);

            return this;
        }

        public int GetPayoutSum()
        {
            int payout = 0;
            if (this.Payments.Count > 0)
            {
                int tempSum = 0;
                List<Payment> sortedPayments = this.Payments.OrderBy(p => p.Date).ToList();
                DateTime initialDate = sortedPayments.First().Date;
                int daysInYear = StaticHelper.GetDaysInYear(initialDate);
                double dayPercent = (double)this.Type.Percent / daysInYear;
                //foreach (var payment in sortedPayments)
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

        public override string ToString()
        {
            return String.Format("{0}, сумма - {1} руб.", Type.Name, this.Sum);
        }

        #endregion
    }
}
