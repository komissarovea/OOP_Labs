using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab6
{
    /// <summary>
    /// Queue
    /// 1.	Дан файл, содержащий информацию о сотрудниках фирмы: 
    /// фамилия, имя, отчество, пол, возраст, размер зарплаты. 
    /// За один просмотр файла напечатать элементы файла в следующем порядке: 
    /// сначала все данные о сотрудниках, зарплата которых меньше 10000, 
    /// потом данные об остальных сотрудниках, 
    /// сохраняя исходный порядок в каждой группе сотрудников. 
    /// </summary>
    public class Task_2_1
    {
        const string INPUT_PATH = "input_2_1.txt";
        const int SALARY_RATE = 10000;

        public static void Run()
        {
            string input = File.ReadAllText(INPUT_PATH, Encoding.GetEncoding(1251));
            Console.WriteLine("Исходный текст: \n" + input);

            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Queue queue = new Queue();

            Console.WriteLine("\nИтоговый текст: ");
            foreach (var line in lines)
            {
                var arr = line.Split(',');
                int salary = int.Parse(arr[3]);
                if (salary < SALARY_RATE)
                    Console.WriteLine(line);
                else
                    queue.Enqueue(line);
            }

            while (queue.Count > 0)
                Console.WriteLine(queue.Dequeue());
        }
    }

    /// <summary>
    /// Queue
    /// 2.	Дан файл, содержащий информацию о сотрудниках фирмы: 
    /// фамилия, имя, отчество, пол, возраст, размер зарплаты. 
    /// За один просмотр файла напечатать элементы файла в следующем порядке: 
    /// сначала все данные о сотрудниках младше 30 лет, 
    /// потом данные об остальных сотрудниках, сохраняя исходный порядок в каждой группе сотрудников. 
    /// </summary>
    public class Task_2_2
    {
        const string INPUT_PATH = "input_2_1.txt";
        const int YEAR_RATE = 30;

        public static void Run()
        {
            string input = File.ReadAllText(INPUT_PATH, Encoding.GetEncoding(1251));
            Console.WriteLine("Исходный текст: \n" + input);

            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Queue queue = new Queue();

            Console.WriteLine("\nИтоговый текст: ");
            foreach (var line in lines)
            {
                var arr = line.Split(',');
                int year = int.Parse(arr[2]);
                if (year < YEAR_RATE)
                    Console.WriteLine(line);
                else
                    queue.Enqueue(line);
            }

            while (queue.Count > 0)
                Console.WriteLine(queue.Dequeue());
        }
    }

    /// <summary>
    /// Queue
    /// 3.	Дан файл, содержащий информацию о студентах: 
    /// фамилия, имя, отчество, номер группы, оценки по трем предметам текущей сессии. 
    /// За один просмотр файла напечатать элементы файла в следующем порядке: 
    /// сначала все данные о студентах, успешно сдавших сессию, 
    /// потом данные об остальных студентах, сохраняя исходный порядок в каждой группе сотрудников. 
    /// </summary>
    public class Task_2_3
    {
        const string INPUT_PATH = "input_2_3.txt";
        const int MIN_GRADE = 3;

        public static void Run()
        {
            string input = File.ReadAllText(INPUT_PATH, Encoding.GetEncoding(1251));
            Console.WriteLine("Исходный текст: \n" + input);

            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Queue queue = new Queue();

            Console.WriteLine("\nИтоговый текст: ");
            foreach (var line in lines)
            {
                var arr = line.Split(',');
                int grade1 = int.Parse(arr[2]);
                int grade2 = int.Parse(arr[3]);
                int grade3 = int.Parse(arr[4]);
                if (grade1 >= MIN_GRADE && grade2 >= MIN_GRADE && grade3 >= MIN_GRADE)
                    Console.WriteLine(line);
                else
                    queue.Enqueue(line);
            }

            while (queue.Count > 0)
                Console.WriteLine(queue.Dequeue());
        }
    }

    /// <summary>
    /// Queue
    /// 4.	Дан файл, содержащий информацию о студентах: 
    /// фамилия, имя, отчество, номер группы, оценки по трем предметам текущей сессии. 
    /// За один просмотр файла напечатать элементы файла в следующем порядке: 
    /// сначала все данные о студентах, успешно обучающихся на 4 и 5, 
    /// потом данные об остальных студентах, сохраняя исходный порядок в каждой группе сотрудников. 
    /// </summary>
    public class Task_2_4
    {
        const string INPUT_PATH = "input_2_3.txt";
        const int MIN_GRADE = 4;

        public static void Run()
        {
            string input = File.ReadAllText(INPUT_PATH, Encoding.GetEncoding(1251));
            Console.WriteLine("Исходный текст: \n" + input);

            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Queue queue = new Queue();

            Console.WriteLine("\nИтоговый текст: ");
            foreach (var line in lines)
            {
                var arr = line.Split(',');
                int grade1 = int.Parse(arr[2]);
                int grade2 = int.Parse(arr[3]);
                int grade3 = int.Parse(arr[4]);
                if (grade1 >= MIN_GRADE && grade2 >= MIN_GRADE && grade3 >= MIN_GRADE)
                    Console.WriteLine(line);
                else
                    queue.Enqueue(line);
            }

            while (queue.Count > 0)
                Console.WriteLine(queue.Dequeue());
        }

    }
}
