using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    /// <summary>
    /// Разработать рекурсивный метод для вывода на экран 
    /// всех возможных разложений натурального числа n на множители(без повторений).
    /// </summary>
    class Samost3
    {
        public static void Run()
        {
            //Map = Select | Enumerable.Range(1, 10).Select(x => x + 2);
            //Reduce = Aggregate | Enumerable.Range(1, 10).Aggregate(0, (acc, x) => acc + x);
            //Filter = Where | Enumerable.Range(1, 10).Where(x => x % 2 == 0);         

            Console.Write("Вывод на экран всех возможных разложений натурального числа n на множители.\nВведите натуральное число: ");
            int n = CommonHelper.ReadPositiveInt();
            PrintProduct(n);
            //Recursion(n, 2);
            //var multipliers = Enumerable.Range(2, n / 2).Where(x => n % x == 0);
            //foreach (var m in multipliers)
            //    Console.WriteLine(m);
            

            Console.ReadLine();
        }

        public static void PrintProduct(int n)
        {
            List<List<int>> list = Decompose(n);
            list.RemoveAt(0);
            if (list.Count == 0)
                Console.WriteLine("Это простое число!");

            foreach (var row in list.OrderByDescending(l => l.Count))
                Console.WriteLine("{0} = {1}", n, string.Join(" * ", row));
        }

        public static List<List<int>> Decompose(int n)
        {
            List<List<int>> list = new List<List<int>>();
            list.Add(new List<int>() { n });
            if (n > 3)
                for (int i = 2; i <= Math.Sqrt(n); i++)
                {
                    if (n % i == 0)
                    {
                        List<List<int>> dec = Decompose(n / i);
                        foreach (var row in dec)
                        {
                            row.Add(i);
                            row.Sort();
                            if (list.FirstOrDefault(l => Enumerable.SequenceEqual(l, row)) == null)
                                list.Add(row);
                        }
                    }
                }
            return list;
        }

        public static void Recursion(int n, int k)
        {
            // k- дополнительный параметр. При вызове должен быть равен 2
            // Базовый случай
            if (k > n / 2)
            {
                Console.WriteLine(n);
                return;
            }
            // Шаг рекурсии / рекурсивное условие
            if (n % k == 0)
            {
                Console.WriteLine(k);
                Recursion(n / k, k);
            } // Шаг рекурсии / рекурсивное условие
            else
            {
                Recursion(n, ++k);
            }
        }

    }
}
