using System;

namespace Samost
{
    public class DepositType
    {
        public int Key { get; set; }

        public string Name { get; set; }

        public int Percent { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - {1}%", this.Name ?? "", this.Percent);
        }
    }
}
