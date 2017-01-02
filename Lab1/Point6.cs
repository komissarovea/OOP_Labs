using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    /// <summary>
    /// 5.	Удалить из сообщения все слова, содержащие данный символ (без учета регистра).
    /// </summary>
    class Point6
    {
        public static void Run()
        {
            Console.WriteLine("Пункт 6. Удалить из сообщения все слова, содержащие данный символ (без учета регистра).");
            Console.WriteLine("Введите строку: ");
            string str = Console.ReadLine();            
            Console.WriteLine("Введите символ: ");
            string symbol = Console.ReadKey().KeyChar.ToString().ToLower();
            Console.WriteLine();                       
            string[] arr = str.Trim().Split(' ');
            string str2 = string.Join<string>(" ", arr.Where(s => !s.ToLower().Contains(symbol)));
            Console.WriteLine("Измененная строка: " + str2);
            Console.ReadLine();
        }
    }
}
