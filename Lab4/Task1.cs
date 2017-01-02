using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab4
{
    /// <summary>
    /// 1.	Создать программу, которая ищет в указанном каталоге файлы, удовлетворяющие заданной маске, 
    /// и дата последней модификации которых находится в указанном диапазоне. 
    /// Поиск производится как в указанном каталоге, так и в его подкаталогах. 
    /// Результаты поиска сбрасываются в файл отчета. 
    /// </summary>
    class Task1
    {
        public static void Run()
        {
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru");

            string path = @"D:\BSUIR";
            //Console.WriteLine("Введите путь к каталогу: ");
            //path = Console.ReadLine();
            DirectoryInfo directory = new DirectoryInfo(path);
            if (!directory.Exists)
            {
                Console.WriteLine("Ошибка! Указаный каталог не существует!");
                return;
            }

            string mask = "*";
            //Console.WriteLine("Введите маску фильтрации файлов: ");
            //mask = Console.ReadLine();

            DateTime start = DateTime.MinValue;
            //Console.WriteLine("Введите дату начала диапазона (дд.мм.гггг): ");
            //start = DateTime.Parse(Console.ReadLine());

            DateTime end = DateTime.MaxValue;
            //Console.WriteLine("Введите дату конца диапазона (дд.мм.гггг): ");
            //end = DateTime.Parse(Console.ReadLine());

            string reportPath = "report.txt";
            //Console.WriteLine("Введите путь для сохранения файла отчёта: ");
            //reportPath = Console.ReadLine();


            StreamWriter sw = new StreamWriter(reportPath, false);
            foreach (var file in directory.EnumerateFiles(mask, SearchOption.AllDirectories))
            {
                if (file.LastWriteTime >= start && file.LastWriteTime <= end)
                {
                    sw.WriteLine(file.FullName);
                    Console.WriteLine(file.FullName);
                }
            }
            sw.Close();
        }

    }
}