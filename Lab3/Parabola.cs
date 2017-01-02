using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Parabola : Function
    {
        private int k;
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

        public Parabola() { }

        public Parabola(int k, int c)
        {
            this.k = k;
            this.c = c;
        }

        public override int Calculate(int x)
        {
            return k * x * x + c;
        }

        public override string ToString()
        {
            return String.Format("Parabola (y = {0}x^2 + {1})", k, c);
        }
    }
}
