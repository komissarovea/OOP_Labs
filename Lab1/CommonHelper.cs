using System;

namespace Lab1
{
    public static class CommonHelper
    {
        public static int[] GenerateArray(int n = 0)
        {
            if (n < 1)
            {
                Console.WriteLine("Введите размерность массива");
                Console.Write("n = ");
                n = ReadPositiveInt();
            }

            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; ++i)
                array[i] = random.Next(-100, 100);
            return array;
        }

        public static int[,] GenerateArrayTD(int n = 0, int m = 0)
        {
            if (n < 1)
            {
                Console.WriteLine("Введите размерность массива");
                Console.Write("n = ");
                n = ReadPositiveInt();
            }
            if (m < 1)
            {
                Console.Write("m = ");
                m = ReadPositiveInt();
            }

            int[,] array = new int[n, m];
            Random random = new Random();
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < m; ++j)
                    array[i, j] = random.Next(-100, 100);
            return array;
        }

        public static int[] InputArray()
        {
            Console.WriteLine("Введите размерность массива:");
            int n = ReadPositiveInt();
            int[] array = new int[n];
            for (int i = 0; i < n; ++i)
            {
                Console.Write("a[{0}]= ", i);
                array[i] = ReadInt();
            }
            return array;
        }

        public static int[,] InputArrayTD()
        {
            Console.WriteLine("Введите размерность массива");
            Console.Write("n = ");
            int n = ReadPositiveInt();
            Console.Write("m = ");
            int m = ReadPositiveInt();
            int[,] array = new int[n, m];
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < m; ++j)
                {
                    Console.Write("a[{0},{1}]= ", i, j);
                    array[i, j] = ReadInt();
                }
            return array;
        }

        public static void PrintArray(int[] array)
        {
            if (array.Length == 0)
                Console.WriteLine(" - ");

            foreach (int t in array)
                Console.Write("{0} ", t);

            Console.WriteLine();
        }

        public static void PrintArray(int[,] array)
        {
            if (array.Length == 0)
                Console.WriteLine(" - ");

            int n = array.GetLength(0); // row count
            int m = array.GetLength(1); // column count
            //for (int i = 0; i < n; ++i, Console.WriteLine())
            //    for (int j = 0; j < m; ++j)
            //        Console.Write("{0,5} ", array[i, j]);
            foreach (var x in array)
                Console.WriteLine(x);
        }

        public static int ReadInt()
        {
            int x = 0;
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out x))
                    break;
            }
            return x;
        }

        public static int ReadPositiveInt()
        {
            int x = 0;
            do
            {
                x = Convert.ToInt32(Console.ReadLine());
            } while (x < 1);
            return x;
        }
    }
}
