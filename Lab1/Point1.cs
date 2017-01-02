using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    /// <summary>
    /// 5. Подсчитать среднее арифметическое элементов (для одномерного массива).
    /// </summary>
    static class Point1
    {
        public static void Run()
        {
            Console.WriteLine("Пункт 1");
            int[] myArray = CommonHelper.InputArray();            
            Console.WriteLine("Исходный массив:");
            CommonHelper.PrintArray(myArray);
            Console.WriteLine("Среднее арифметическое: {0}", GetAverage(myArray));
            Console.ReadLine();
        }

        static double GetAverage(int[] a)
        {
            return a.Average();
            //double sum = a.Sum();
            //int count = a.Count();
            //return sum/count;
        }

    }
}
