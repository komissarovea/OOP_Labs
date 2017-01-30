using System;

namespace Samost
{
    public class Payment
    {
        public int Sum { get; set; }    

        public DateTime Date { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - {1} руб.", this.Date, this.Sum);
        }
    }
}
