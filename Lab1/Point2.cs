using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    /// <summary>
    /// 5. Вывести на экран номера всех элементов, не совпадающих с максимальным.
    /// </summary>
    static class Point2
    {
        public static void Run()
        {
            Console.WriteLine("Пункт 2");
            int[] myArray = CommonHelper.InputArray();
            int max = myArray.Max();

            Console.WriteLine("Номера всех элементов, не совпадающих с максимальным: ");
            for (int i = 0; i < myArray.Length; ++i)
                if (myArray[i] != max)
                    Console.WriteLine(i);
            Console.ReadLine();
        }

        static int Max(int[] a)
        {
            int max = a[0];
            for (int i = 1; i < a.Length; ++i)
                if (a[i] > max) max = a[i];
            return max;
        }
    }
}
