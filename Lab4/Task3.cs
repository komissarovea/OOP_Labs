using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    /// <summary>
    /// 3.	Создать программу для поиска по всему диску файлов и каталогов, удовлетворяющих заданной маске. 
    /// Необходимо вывести найденную информацию на экран в компактном виде (с нумерацией объектов) 
    /// и запросить у пользователя о дальнейших действиях. Варианты действий: удалить все найденное, удалить указанный файл (каталог), удалить диапазон файлов (каталогов). 
    /// </summary>
    class Task3
    {
        public static void Run()
        {
            //string path = @"D:\";
            string path = @"D:\BSUIR\Test";
            //Console.WriteLine("Введите путь к каталогу: ");
            //path = Console.ReadLine();
            DirectoryInfo directory = new DirectoryInfo(path);
            if (!directory.Exists)
            {
                Console.WriteLine("Ошибка! Указаный каталог не существует!");
                return;
            }

            string mask = "*.txt";
            //Console.WriteLine("Введите маску фильтрации файлов: ");
            //mask = Console.ReadLine();

            int n = 1;
            Dictionary<int, FileInfo> dict = new Dictionary<int, FileInfo>();
            foreach (var file in directory.EnumerateFiles(mask, SearchOption.AllDirectories))
            {
                dict.Add(n, file);
                Console.WriteLine("{0}) {1}", n, file.FullName);
                n++;
            }

            Console.WriteLine("\nВыберите действие: \n1 - удалить все найденное \n2 - удалить указанный файл (каталог) \n3 - удалить диапазон файлов (каталогов)");
            int choice = 0;
            while (choice < 1 || choice > 3)
            {
                int.TryParse(Console.ReadLine(), out choice);
            }

            switch (choice)
            {
                case 1:
                    foreach (var pair in dict)
                    {
                        pair.Value.Delete();
                    }
                    Console.WriteLine("Все найденные файлы удалены.");
                    break;
                case 2:
                    Console.WriteLine("Выберите номер файла:");
                    if (int.TryParse(Console.ReadLine(), out choice) && dict.ContainsKey(choice))
                    {
                        dict[choice].Delete();
                        Console.WriteLine("Указанный файл удалён.");
                    }
                    else
                        Console.WriteLine("Указанный номер отсутствует.");
                    break;
                case 3:
                    Console.WriteLine("Введите старт диапазона:");
                    int start = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введите конец диапазона:");
                    int end = int.Parse(Console.ReadLine());
                    for (int i = start; i <= end; i++)
                    {
                        dict[i].Delete();
                    }
                    Console.WriteLine("Указанныe файлы удалены.");
                    break;
            }

        }

    }
}