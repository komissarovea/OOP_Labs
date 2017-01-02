using System;
using System.Text;

namespace Lab1
{
    /// <summary>
    /// 5.	определяет, имеются ли в строке два соседствующих одинаковых символа;
    /// </summary>
    class Point5
    {
        public static void Run()
        {
            Console.WriteLine("Пункт 5");
            Console.WriteLine("Введите строку: ");
            StringBuilder a = new StringBuilder(Console.ReadLine());
            Console.WriteLine("Исходная строка: " + a);

            bool repeatExists = false;
            for (int i = 0; i < a.Length - 1; ++i)
                if (a[i] == a[i+1])
                {
                    repeatExists = true;
                    break;
                }
            Console.WriteLine(repeatExists ? "В строке есть два соседствующих одинаковых символа" 
                : "В строке нет двух соседствующих одинаковых символов");
            Console.ReadLine();
        }
    }
}
