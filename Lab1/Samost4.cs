using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    /// <summary>
    /// Разработать рекурсивный метод для вывода на экран 
    /// всех возможных разложений натурального числа n на слагаемые(без повторений).
    /// </summary>
    class Samost4
    {
        public static void Run()
        {
            Console.Write("Вывод на экран всех возможных разложений натурального числа n на слагаемые.\nВведите натуральное число: ");
            int n = CommonHelper.ReadPositiveInt();

            //PrintSum(n, 1, 1);
            PrintSum2(n);
            //Recurs(n, 1, n, "");

            Console.ReadLine();
        }

        public static void PrintSum2(int n)
        {
            List<List<int>> list = Decompose(n);
            list.RemoveAt(0);
            foreach (var row in list.OrderByDescending(l => l.Count))
                Console.WriteLine("{0} = {1}", n, string.Join(" + ", row));            
        }

        public static List<List<int>> Decompose(int n)
        {
            List<List<int>> list = new List<List<int>>();
            list.Add(new List<int>() { n });
            if (n > 1)
            {
                for (int i = 1; i <= n / 2; i++)
                {
                    List<List<int>> dec = Decompose(n - i);
                    foreach (var row in dec)
                    {
                        row.Add(i);
                        row.Sort();
                        // row.Sort((n1, n2) => n2.CompareTo(n1));
                        if (list.FirstOrDefault(l => Enumerable.SequenceEqual(l, row)) == null)
                            list.Add(row);
                    }
                }
            }
            return list;
        }

        public static void PrintSum(int n, int first, int last)
        {
            if (n <= 1)
                Console.Write(n + " = " + n);
            else if (first + last <= n)
            {
                Console.Write(first);
                for (int i = 0; i < n - first - last; i++)
                    Console.Write(" + 1");
                Console.WriteLine(" + {0} = {1}", last, n);
                PrintSum(n, first, last + 1);
            }
            else
            {
                int newFirst = first + 1;
                if (n >= newFirst * 2)
                    PrintSum(n, newFirst, newFirst);
            }
        }

        static void Recurs(int basic, int start, int diff, string res)
        {
            if (diff == 0)
            {
                Console.WriteLine(res + " = " + basic);
                return;
            }

            int item = start;
            while (item <= diff && item != basic)
            {
                Recurs(basic, item, diff - item, res == "" ? res + item : res + " + " + item);
                item++;
            }
        }
    }

}
