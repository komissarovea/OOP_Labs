using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    /// <summary>
    /// 2.	Создать программу для поиска указанного текста в файлах, удовлетворяющих заданной маске, 
    /// и замене этого тектса на другой указанный текст. 
    /// Поиск производится как в указанном каталоге, так и в его подкаталогах. 
    /// </summary>
    class Task2
    {
        public static void Run()
        {
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

            string searchText = "searchText";
            //Console.WriteLine("Введите текст для поиска: ");
            //searchText = Console.ReadLine();

            string newText = "newText";
            //Console.WriteLine("Введите текст для замены: ");
            //newText = Console.ReadLine();

            foreach (var file in directory.EnumerateFiles(mask, SearchOption.AllDirectories))
            {
                Console.WriteLine(file.FullName);
                string fileContent = File.ReadAllText(file.FullName);
                fileContent = fileContent.Replace(searchText, newText);
                File.WriteAllText(file.FullName, fileContent);
            }
        }

    }
}