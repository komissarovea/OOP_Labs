using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    class Samost1
    {
        public static void Run()
        {
            Console.WriteLine("Самостоятельная работа 1");

            int[] array1 = CommonHelper.GenerateArray(10);
            Console.WriteLine("Исходный массив:");
            CommonHelper.PrintArray(array1);

            Task1(array1);
            Task2(array1);
            Task3(array1);
            Task4(array1);
            Task5(array1);

            Console.ReadLine();
        }

        public static void Task1(int[] array1)
        {
            Console.WriteLine("\n1.Удалить из массива все четные числа.");
            int[] array2 = array1.Where(n => n % 2 != 0).ToArray();
            Console.WriteLine("Измененный массив:");
            CommonHelper.PrintArray(array2);
            Console.ReadLine();
        }

        public static void Task2(int[] array1)
        {
            Console.WriteLine("\n2.Вставить новый элемент после всех элементов, которые заканчиваются на данную цифру.");
            Console.WriteLine("Введите цифру: ");
            int x = CommonHelper.ReadPositiveInt();
            int[] array2 = array1.SelectMany(n =>
            {
                List<int> retval = new List<int>() { n };
                if (Math.Abs(n) % 10 == x)
                {
                    Console.WriteLine("Введите новый элемент: ");
                    retval.Add(CommonHelper.ReadInt());
                }
                return retval;
            }).ToArray();
            //    var matches = array1.Where(n => Math.Abs(n) % 10 == x);
            //    int newCount = array1.Length + matches.Count();
            //    int[] array2 = new int[newCount];
            //    for (int i = 0, j = 0, n = array1.Length; i < n; i++, j++)
            //    {
            //        array2[j] = array1[i];
            //        if (matches.Contains(array1[i]))
            //        {
            //            Console.WriteLine("Введите новый элемент: ");
            //            array2[++j] = CommonHelper.ReadInt();
            //        }                    
            //    }
            Console.WriteLine("Измененный массив:");
            CommonHelper.PrintArray(array2);
            Console.ReadLine();
        }

        public static void Task3(int[] array1)
        {
            Console.WriteLine("\n3.Удалить из массива повторяющиеся элементы, оставив только их первые вхождения");
            int[] array2 = array1.Distinct().ToArray();
            Console.WriteLine("Измененный массив:");
            CommonHelper.PrintArray(array2);
            Console.ReadLine();
        }

        public static void Task4(int[] array1)
        {
            Console.WriteLine("\n4.Вставить новый элемент между всеми парами элементов, имеющими разные знаки.");
            int[] array2 = array1.SelectMany((n, i) =>
            {
                List<int> retval = new List<int>() { n };
                if (i != array1.Length - 1 && n * array1[i + 1] < 0)
                {
                    Console.WriteLine("Введите новый элемент: ");
                    retval.Add(CommonHelper.ReadInt());
                }
                return retval;
            }).ToArray();

            Console.WriteLine("Измененный массив:");
            CommonHelper.PrintArray(array2);
            Console.ReadLine();
        }

        public static void Task5(int[] array1)
        {
            Console.WriteLine("\n5.Уплотнить массив, удалив из него все нулевые значения.");
            int[] array2 = array1.Where(n => n != 0).ToArray();
            Console.WriteLine("Измененный массив:");
            CommonHelper.PrintArray(array2);
        }
    }
}
