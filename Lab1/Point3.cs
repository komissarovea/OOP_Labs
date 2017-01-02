using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    /// <summary>
    /// 5.	Поменять местами столбцы по правилу: первый с последним, второй с предпоследним и т.д.
    /// </summary>
    class Point3
    {
        public static void Run()
        {
            Console.WriteLine("Пункт 3. Поменять местами столбцы по правилу: первый с последним, второй с предпоследним и т.д.");
            int[,] myArray = CommonHelper.InputArrayTD();
            Console.WriteLine("Исходный массив:");
            CommonHelper.PrintArray(myArray);
            myArray = SwapColumns(myArray);
            Console.WriteLine("Измененный массив:");
            CommonHelper.PrintArray(myArray);
            Console.ReadLine();
        }

        static int[,] SwapColumns(int[,] a)
        {
            int n = a.GetLength(0);
            int m = a.GetLength(1);

            int[,] a2 = new int[n, m];
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < m; ++j)
                    a2[i, j] = a[i, m - 1 - j];
            return a2;
        }

    }
}
