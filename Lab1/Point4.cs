using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    /// <summary>
    /// 5.	Для каждой строки подсчитать  количество положительных элементов и записать данные в новый массив.
    /// </summary>
    class Point4
    {
        public static void Run()
        {
            Console.WriteLine("Пункт 4. Для каждой строки подсчитать количество положительных элементов и записать данные в новый массив.");
            int[][] myArray = Input();
            Console.WriteLine("Исходный массив:");
            Print2(myArray);
            int[] rez = new int[myArray.Length];
            for (int i = 0; i < myArray.Length; ++i)
                rez[i] = GetPositivesCount(myArray[i]);
            Console.WriteLine("Новый массив:");
            Print1(rez);
            Console.ReadLine();
        }

        static int[][] Input()
        {
            Console.WriteLine("Введите размерность массива");
            Console.Write("n = ");
            int n = int.Parse(Console.ReadLine());
            int[][] a = new int[n][];
            for (int i = 0; i < n; ++i)
            {
                a[i] = new int[n];
                for (int j = 0; j < n; ++j)
                {
                    Console.Write("a[{0},{1}]= ", i, j);
                    a[i][j] = int.Parse(Console.ReadLine());
                }
            }
            return a;
        }

        static void Print1(int[] a)
        {
            for (int i = 0; i < a.Length; ++i)
                Console.Write("{0,5} ", a[i]);
        }

        static void Print2(int[][] a)
        {
            for (int i = 0; i < a.Length; ++i, Console.WriteLine())
                for (int j = 0; j < a[i].Length; ++j)
                    Console.Write("{0,5} ", a[i][j]);
        }

        static int GetPositivesCount(int[] a)
        {
            return a.Count(x => x > 0);
        }

    }
}
