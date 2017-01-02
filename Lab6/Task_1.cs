using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    /// <summary>
    /// Stack
    /// 1.	Дан файл, в котором записан набор чисел. 
    /// Переписать в другой файл все числа в обратном порядке. 
    /// </summary>
    public class Task_1_1
    {
        const string INPUT_PATH = "input_1_1.txt";
        const string OUTPUT_PATH = "output_1_1.txt";

        public static void Run()
        {
            string input = File.ReadAllText(INPUT_PATH);
            Console.WriteLine("Исходный текст: " + input);

            Stack stack = new Stack();
            var splitted = input.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in splitted)
                stack.Push(item);

            List<string> reversed = new List<string>();
            while(stack.Count > 0)
                reversed.Add(stack.Pop().ToString());

            string output = string.Join(" ", reversed);
            Console.WriteLine("Итоговый текст: " + output);
            File.WriteAllText(OUTPUT_PATH, output);
        }
    }

    /// <summary>
    /// Stack
    /// 2.	Создать текстовый файл. 
    /// Распечатать гласные буквы этого файла в обратном порядке.
    /// </summary>
    public class Task_1_2
    {
        const string INPUT_PATH = "input_1_2.txt";
        static readonly char[] VOWELS = new char[] { 'а', 'о', 'и', 'е', 'ё', 'э', 'ы', 'у', 'ю', 'я', };

        public static void Run()
        {
            string input = File.ReadAllText(INPUT_PATH, Encoding.GetEncoding(1251));
            Console.WriteLine("Исходный текст: " + input);

            Stack stack = new Stack();
            foreach (var symbol in input)
                if (VOWELS.Contains(symbol))
                    stack.Push(symbol);

            List<string> reversed = new List<string>();
            while (stack.Count > 0)
                reversed.Add(stack.Pop().ToString());

            string output = string.Join(" ", reversed);
            Console.WriteLine("Гласные буквы в обратном порядке: " + output);
            //File.WriteAllText(OUTPUT_PATH, output);
        }
    }

    /// <summary>
    /// Stack
    /// 3.	Напечатать содержимое текстового файла t, 
    /// выписывая литеры каждой его строки в обратном порядке. 
    /// </summary>
    public class Task_1_3
    {
        const string INPUT_PATH = "input_1_3.txt";

        public static void Run()
        {
            string input = File.ReadAllText(INPUT_PATH, Encoding.GetEncoding(1251));
            Console.WriteLine("Исходный текст: " + input);

            Stack stack = new Stack();
            foreach (var symbol in input)
                stack.Push(symbol);

            List<string> reversed = new List<string>();
            while (stack.Count > 0)
                reversed.Add(stack.Pop().ToString());

            string output = string.Join("", reversed);
            Console.WriteLine("Текст в обратном порядке: " + output);
            //File.WriteAllText(OUTPUT_PATH, output);
        }
    }

    /// <summary>
    /// Stack
    /// 4.	Даны 2 строки s1 и s2. Из каждой можно читать по одному символу. 
    /// Выяснить, является ли строка s2 обратной s1.  
    /// </summary>
    public class Task_1_4
    {
        const string INPUT_PATH = "input_1_3.txt";

        public static void Run()
        {
            Console.WriteLine("Введите строку s1: ");
            string s1 = Console.ReadLine();

            Console.WriteLine("Введите строку s2: ");
            string s2 = Console.ReadLine();

            Stack stack = new Stack();
            foreach (var symbol in s1)
                stack.Push(symbol);

            bool isReverse = s1.Length == s2.Length;
            if (isReverse)
                for (int i = 0; i < s2.Length; i++)
                {
                    if (s2[i] != (char)stack.Pop())
                    {
                        isReverse = false;
                        break;
                    }
                }

            Console.WriteLine("Строка s2 {0}является обратной s1", isReverse ? "" : "не ");
        }
    }
}
