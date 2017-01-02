using Samost.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samost
{
    public class Client
    {
        #region Fields

        private List<Deposit> _deposits = new List<Deposit>();
        private string _name;

        #endregion

        #region Properties

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

        public List<Deposit> Deposits { get { return _deposits; } }

        public int PercentBonus { get; set; }

        public int FixBonus { get; set; }

        #endregion

        #region Methods

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

        public override string ToString()
        {
            return String.Format("{0}, бонус в процентах - {1}%, фиксированный бонус - {2}",
                this.Name, this.PercentBonus, this.FixBonus);
        }

        #endregion
    }
}
