using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Line : Function
    {
        private int k;
        private int b;

        public int K
        {
            get
            {
                return k;
            }

            set
            {
                k = value;
            }
        }

        public int B
        {
            get
            {
                return b;
            }

            set
            {
                b = value;
            }
        }

        public Line() { }

        public Line(int k, int b)
        {
            this.k = k;
            this.b = b;
        }

        public override int Calculate(int x)
        {
            return k * x + b;
        }

        public override string ToString()
        {
            return String.Format("Line (y = {0}x + {1})", k, b);
        }
    }
}
