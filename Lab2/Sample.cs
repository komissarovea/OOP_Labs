using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab2
{
    class DemoArray
    {
        int[] MyArray;//закрытый массив

        public DemoArray(int size)//конструктор 1
        {
            MyArray = new int[size];
        }

        public DemoArray(params int[] arr)//конструктор 2
        {
            MyArray = new int[arr.Length];
            for (int i = 0; i < MyArray.Length; i++) MyArray[i] = arr[i];
        }

        public int LengthArray //свойство, возвращающее размерность
        {
            get { return MyArray.Length; }
        }

        public int this[int i] //индексатор
        {
            get
            {
                if (i < 0 || i >= MyArray.Length) throw new Exception("выход за границы массива");
                return MyArray[i];
            }
            set
            {
                if (i < 0 || i >= MyArray.Length) throw new Exception("выход за границы массива");
                else MyArray[i] = value;
            }
        }

        public static DemoArray operator -(DemoArray x) //перегрузка операции унарный минус
        {
            DemoArray temp = new DemoArray(x.LengthArray);
            for (int i = 0; i < x.LengthArray; ++i)
                temp[i] = -x[i];
            return temp;
        }

        public static DemoArray operator ++(DemoArray x) //перегрузка операции инкремента
        {
            DemoArray temp = new DemoArray(x.LengthArray);
            for (int i = 0; i < x.LengthArray; ++i)
                temp[i] = x[i] + 1;
            return temp;
        }

        public static bool operator true(DemoArray a) //перегрузка константы true
        {
            foreach (int i in a.MyArray)
            {
                if (i < 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator false(DemoArray a)//перегрузка константы false
        {
            foreach (int i in a.MyArray)
            {
                if (i > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static DemoArray operator +(DemoArray x, int a) //вариант 1
        {
            DemoArray temp = new DemoArray(x.LengthArray);
            for (int i = 0; i < x.LengthArray; ++i)
                temp[i] = x[i] + a;
            return temp;
        }

        public static DemoArray operator +(DemoArray x, DemoArray y) //вариант 2
        {
            if (x.LengthArray == y.LengthArray)
            {
                DemoArray temp = new DemoArray(x.LengthArray);
                for (int i = 0; i < x.LengthArray; ++i)
                    temp[i] = x[i] + y[i];
                return temp;
            }
            else throw new Exception("несоответствие размерностей");
        }

        public static implicit operator DemoArray(int[] a) //неявное преобразование типа int [] в DemoArray
        {
            return new DemoArray(a);
        }

        public static implicit operator int[](DemoArray a) //неявное преобразование типа DemoArray в int []
        {
            int[] temp = new int[a.LengthArray];
            for (int i = 0; i < a.LengthArray; ++i)
                temp[i] = a[i];
            return temp;
        }

        public void Print(string name) //метод - выводит поле-массив на экран
        {
            Console.WriteLine(name + ": ");
            for (int i = 0; i < MyArray.Length; i++)
                Console.Write(MyArray[i] + " ");
            Console.WriteLine();
        }
    }

    class Program
    {
        static void arrayPrint(string name, int[] a) //метод, который позволяет вывести на экран одномерный массив
        {
            Console.WriteLine(name + ": ");
            for (int i = 0; i < a.Length; i++)
                Console.Write(a[i] + " ");
            Console.WriteLine();
        }

        static void Main2()
        {
            try
            {
                DemoArray a = new DemoArray(1, -4, 3, -5, 0);
                int[] mas1 = a; //неявное преобразование типа DemoArray в int []
                int[] mas2 = (int[])a; //явное преобразование типа DemoArray в int []
                DemoArray b1 = mas1; //неявное преобразование типа int [] в DemoArray    
                DemoArray b2 = (DemoArray)mas2; //явное преобразование типа int [] в DemoArray

                //изменение значений
                mas1[0] = 0; mas2[0] = -1;
                b1[0] = 100; b2[0] = -100;

                //вывод на экран
                a.Print("Массива a");
                arrayPrint("Maccив mas1", mas1);
                arrayPrint("Maccив mas2", mas2);
                b1.Print("Массива b1");
                b2.Print("Массива b2");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }

}
