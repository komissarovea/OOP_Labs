/// Комиссаров Евгений Александрович, 50326-2, вариант 5
using System; // подключение общей библиотеки классов
using System.Collections.Generic; // подключение библиотеки классов, определяющих типизированные коллекции
using System.Linq; // пространство имен System.Linq содержит классы и интерфейсы, которые поддерживают запросы, использующие LINQ (Language-Integrated Query)

namespace Samost
{
    /// <summary>
    /// Вклад
    /// </summary>
    public class Deposit
    {
        #region Fields

        const int MIN_PAYMENT = 100; // поле для хранения величины платежа, на которую нужно увеличить вклад при использовании оператора "++"

        private List<Payment> _payments = new List<Payment>(); // поле для хранения списка платежей данного вклада

        #endregion

        #region  Properties

        /// <summary>
        /// Тип вклада
        /// </summary>
        public DepositType Type { get; set; } // задать или считать значение

        /// <summary>
        /// Список платежей по вкладу
        /// </summary>
        public List<Payment> Payments
        {
            get { return _payments; } // задать или считать значение
        }

        /// <summary>
        /// Общая сумма вклада
        /// </summary>
        public int Sum
        {
            get { return _payments.Sum(p => p.Sum); } // вернуть сумму платежей по вкладу
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
            return deposit.Replenish(MIN_PAYMENT); // Пополнить вклад на MIN_PAYMENT
        }

        /// <summary>
        /// Перегрузка оператора "+" для пополнения вклада на заданную сумму
        /// </summary>
        /// <param name="deposit"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        public static Deposit operator +(Deposit deposit, int payment)
        {
            return deposit.Replenish(payment); // Пополнить вклад на заданную сумму
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
            if (this.Sum + paymentSum < 0) // прооверить: Сумма вклада после проведения операции отрицательна?
                throw new DepositException("Ошибка! Величина вклада не может быть отрицательной!"); // сгенерировать ошибку DepositException
            Payment payment = new Payment() { Sum = paymentSum, Date = DateTime.Now }; // создать объект с заданной суммой и сегодняшней датой
            this.Payments.Add(payment); // добавить созданный объект к сумме платежей

            return this; // вернуть текущий объект (для перегрузки операторов)
        }

        /// <summary>
        /// Вернуть сумму выплат по вкладу
        /// </summary>
        /// <returns></returns>
        public int GetPayoutSum()
        {
            int payout = 0; // инициализация переменной payout
            if (this.Payments.Count > 0) // проверить на существование хоть одного платежа
            {
                int tempSum = 0; // инициализация переменной tempSum
                List<Payment> sortedPayments = this.Payments.OrderBy(p => p.Date).ToList(); // сортировать список платежей по дате, конвертация в List
                DateTime initialDate = sortedPayments.First().Date; // определить дату первого платежа
                int daysInYear = GetDaysInYear(initialDate); // определить количество дней в году 
                double dayPercent = (double)this.Type.Percent / daysInYear; // определить процент в расчёте на день
                int count = this.Payments.Count; // инициализация переменной count (количество платежей)
                for (int i = 0; i < count; i++) // вычисление суммы выплат с учётом внесённых платежей
                {
                    tempSum += sortedPayments[i].Sum; // вычисление базы для расчёта процентов
                    DateTime nextDate = i + 1 == count ? DateTime.Now : sortedPayments[i + 1].Date; // вычисление даты следующего платежа (или текущей даты)
                    int days = (nextDate - sortedPayments[i].Date).Days; // вычисление количества дней действия базы для расчёта процентов
                    payout += (int)(days * dayPercent * tempSum / 100); // добавление процентов к общей сумме выплат
                }
            }
            return payout; // вернуть payout
        }

        /// <summary>
        /// Вернуть количество дней в году для заданной даты
        /// </summary>
        /// <param name="thisDate"></param>
        /// <returns></returns>
        private int GetDaysInYear(DateTime thisDate)
        {
            var nextDate = thisDate.AddYears(1); // получить аналогичную дату для следующего года
            return (nextDate - thisDate).Days; // вернуть разницу, посчитанную в днях
        }

        /// <summary>
        /// Вернуть строку, которая описывает данный объект
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0}, сумма - {1} руб.", Type.Name, this.Sum); // вернуть форматированную строку
        }

        #endregion
    }
}
