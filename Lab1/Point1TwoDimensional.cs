using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    /// <summary>
    /// 5.	Подсчитать среднее арифметическое элементов (для двумерного массива).
    /// </summary>
    static class Point1TwoDimensional
    {
        public static void Run()
        {
            Console.WriteLine("Пункт 1 (для двумерного массива)");
            int[,] myArray = CommonHelper.InputArrayTD();
            Console.WriteLine("Исходный массив:");
            CommonHelper.PrintArray(myArray);
            Console.WriteLine("Среднее арифметическое: {0}", GetAverage(myArray));
            Console.ReadLine();
        }

        static double GetAverage(int[,] a)
        {
            double sum = 0;
            for (int i = 0; i < a.GetLength(0); ++i)
                for (int j = 0; j < a.GetLength(1); ++j)
                    sum += a[i, j];
            int count = a.Length;
            return sum / count;
        }
    }
}