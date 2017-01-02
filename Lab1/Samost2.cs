using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    class Samost2
    {
        public static void Run()
        {
            Console.WriteLine("Самостоятельная работа 2");
            //int[,] array1 = CommonHelper.InputArrayTD();
            int[,] array1 = CommonHelper.GenerateArrayTD();
            Console.WriteLine("Исходный массив:");
            CommonHelper.PrintArray(array1);
            int n = array1.GetLength(0); // row count
            int m = array1.GetLength(1); // column count

            Task1(array1, n, m);
            Task2(array1, n, m);
            Task3(array1, n, m);
            Task4(array1, n, m);
            Task5(array1, n, m);
            Task6(array1, n, m);
        }

        static void Task1(int[,] array1, int n, int m)
        {
            Console.WriteLine("\n1.Вставить новую строку после строки, в которой находится первый встреченный минимальный элемент.");
            int[] flatArr = array1.Cast<int>().ToArray();
            int min = flatArr.Min();
            Console.WriteLine("Минимальный элемент: {0}", min);
            int minRowIndex = Array.IndexOf(flatArr, min) / m;

            int[,] array2 = new int[n + 1, m];
            for (int i = 0, k = 0; i < n; ++i, ++k)
            {
                for (int j = 0; j < m; ++j)
                    array2[k, j] = array1[i, j];

                if (i == minRowIndex)
                {
                    k++;
                    for (int j = 0; j < m; ++j)
                        array2[k, j] = 1;
                }
            }
            Console.WriteLine("Измененный массив:");
            CommonHelper.PrintArray(array2);
            Console.ReadLine();
        }

        static void Task2(int[,] array1, int n, int m)
        {
            Console.WriteLine("\n2.Вставить новый столбец перед всеми столбцами, в которых встречается заданное число.");
            Console.WriteLine("Введите число: ");
            int x = CommonHelper.ReadInt();
            var columns = Enumerable.Range(0, m).Where(j =>
            {
                for (int i = 0; i < n; ++i)
                    if (array1[i, j] == x)
                        return true;
                return false;
            });

            int[,] array2 = new int[n, m + columns.Count()];
            for (int j = 0, k = 0; j < n; ++j, ++k)
            {
                if (columns.Contains(j))
                {
                    for (int i = 0; i < m; ++i)
                        array2[i, k] = 1;
                    k++;
                }

                for (int i = 0; i < m; ++i)
                    array2[i, k] = array1[i, j];
            }
            Console.WriteLine("Измененный массив:");
            CommonHelper.PrintArray(array2);
            Console.ReadLine();
        }

        static void Task3(int[,] array1, int n, int m)
        {
            Console.WriteLine("\n3.Удалить все строки, в которых нет ни одного четного элемента.");
            var rows = Enumerable.Range(0, n).Where(i =>
            {
                for (int j = 0; j < m; ++j)
                    if (array1[i, j] % 2 == 0)
                        return true;
                return false;
            });

            int[,] array2 = new int[rows.Count(), m];
            for (int i = 0, k = 0; i < n; ++i)
            {
                if (rows.Contains(i))
                {
                    for (int j = 0; j < m; ++j)
                        array2[k, j] = array1[i, j];
                    k++;
                }
            }
            Console.WriteLine("Измененный массив:");
            CommonHelper.PrintArray(array2);
            Console.ReadLine();
        }

        static void Task4(int[,] array1, int n, int m)
        {
            Console.WriteLine("\n4.Удалить все столбцы, в которых все элементы положительны.");
            var columns = Enumerable.Range(0, m).Where(j =>
            {
                for (int i = 0; i < n; ++i)
                    if (array1[i, j] <= 0)
                        return true;
                return false;
            });

            int[,] array2 = new int[n, columns.Count()];
            for (int j = 0, k = 0; j < n; ++j)
            {
                if (columns.Contains(j))
                {
                    for (int i = 0; i < m; ++i)
                        array2[i, k] = array1[i, j];
                    k++;
                }
            }
            Console.WriteLine("Измененный массив:");
            CommonHelper.PrintArray(array2);
            Console.ReadLine();
        }

        static void Task5(int[,] array1, int n, int m)
        {
            Console.WriteLine("\n5.Удалить из массива k - тую строку и j - тый столбец, если их значения совпадают.");
            if (n != m)
            {
                Console.WriteLine("Количество строк и столбцов не совпадает.");
                return;
            }

            var rows = Enumerable.Range(0, n).ToList();
            var columns = Enumerable.Range(0, n).ToList();
            for (int i = 0; i < n; ++i)
            {
                var tempColumns = Enumerable.Range(0, n).ToList();
                for (int j = 0; j < m; ++j)
                {
                    int cell = array1[i, j];
                    for (int k = 0; k < m; ++k)
                    {
                        if (cell != array1[j, k])
                            tempColumns.Remove(k);
                    }
                }
                if (tempColumns.Count > 0)
                {
                    rows.Remove(i);
                    columns.RemoveAll(x => tempColumns.Contains(x));
                }
            }

            int[,] array2 = new int[rows.Count(), columns.Count()];
            {
                int i = 0, j = 0;
                foreach (var row in rows)
                {
                    foreach (var column in columns)
                    {
                        array2[i, j] = array1[row, column];
                        j++;
                    }
                    j = 0;
                    i++;
                }
            }
            Console.WriteLine("Измененный массив:");
            CommonHelper.PrintArray(array2);
            Console.ReadLine();
        }

        static void Task6(int[,] array1, int n, int m)
        {
            Console.WriteLine("\n6.Уплотнить массив, удалив из него все нулевые строки и столбцы. ");
            var rows = Enumerable.Range(0, n).Where(i =>
            {
                for (int j = 0; j < m; ++j)
                    if (array1[i, j] != 0)
                        return true;
                return false;
            });
            var columns = Enumerable.Range(0, m).Where(j =>
            {
                for (int i = 0; i < n; ++i)
                    if (array1[i, j] != 0)
                        return true;
                return false;
            });

            int[,] array2 = new int[rows.Count(), columns.Count()];
            {
                int i = 0, j = 0;
                foreach (var row in rows)
                {
                    foreach (var column in columns)
                    {
                        array2[i, j] = array1[row, column];
                        j++;
                    }
                    j = 0;
                    i++;
                }
            }
            Console.WriteLine("Измененный массив:");
            CommonHelper.PrintArray(array2);
            Console.ReadLine();
        }
    }
}
