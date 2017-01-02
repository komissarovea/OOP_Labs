using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Kub : Function
    {
        private int k;
        private int b;
        private int c;

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

        public int C
        {
            get
            {
                return c;
            }

            set
            {
                c = value;
            }
        }

        public Kub() { }

        public Kub(int k, int b, int c)
        {
            this.k = k;
            this.b = b;
            this.c = c;
        }

        public override int Calculate(int x)
        {
            return k * x * x + b * x + c;
        }

        public override string ToString()
        {
            return String.Format("Kub (y = {0}x^2 + {1}x + {2})", k, b, c);
        }
    }
}
