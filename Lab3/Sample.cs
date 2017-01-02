using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    abstract class Demo //абстрактный класс
    {
        abstract public void Show();//абстрактный метод
        abstract public double Dlina();//абстрактный метод
    }

    class DemoPoint : Demo //производный класс от абстрактного
    {
        protected int x;
        protected int y;
        public DemoPoint(int x, int y)
        {
            this.x = x; this.y = y;
        }
        public override void Show() //переопределение абстрактного метода
        {
            Console.WriteLine("точка на плоскости: ({0}, {1})", x, y);
        }
        public override double Dlina()  //переопределение абстрактного метода
        {
            return Math.Sqrt(x * x + y * y);
        }
    }

    class DemoShape : DemoPoint //производный класс 
    {
        protected int z;
        public DemoShape(int x, int y, int z) : base(x, y)
        {
            this.z = z;
        }
        public override void Show()  //переопределение абстрактного метода
        {
            Console.WriteLine("точка в пространстве: ({0}, {1}, {2})", x, y, z);
        }
        public override double Dlina()  //переопределение абстрактного метода
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }
    }

    class DemoLine : DemoPoint //производный класс
    {
        protected int x2;
        protected int y2;
        public DemoLine(int x1, int y1, int x2, int y2) : base(x1, y1)
        {
            this.x2 = x2; this.y2 = y2;
        }
        public override void Show()  //переопределение абстрактного метода
        {
            Console.WriteLine("отрезок на плоскости: ({0}, {1})-({2},{3})", x, y, x2, y2);
        }
        public override double Dlina()  //переопределение абстрактного метода
        {
            return Math.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
        }
    }

    class Program
    {
        static void Main2()
        {
            Demo[] Ob = new Demo[5]; //массив ссылок
                                     //заполнения массива ссылками на объекты производных классов
            Ob[0] = new DemoPoint(1, 1);
            Ob[1] = new DemoShape(1, 1, 1);
            Ob[2] = new DemoLine(0, 3, 4, 0);
            Ob[3] = new DemoLine(2, 1, 2, 10);
            Ob[4] = new DemoPoint(0, 100);
            foreach (Demo a in Ob) //просмотр массива
            {
                a.Show();
                Console.WriteLine("Dlina: {0:f2}\n", a.Dlina());
            }

            Console.ReadLine(); 
        }
    }


}
