using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class Task_4
    {
        public static void Run()
        {
            Hashtable catalog = new Hashtable();
            FillCatalogWithDefaultAlbums(catalog);

            int choice = -1;
            while (choice != 8)
            {
                Console.Clear();
                Console.WriteLine("МЕНЮ:");
                Console.WriteLine("1) добавить диск;");
                Console.WriteLine("2) удалить диск;");
                Console.WriteLine("3) добавить песню;");
                Console.WriteLine("4) удалить песню;");
                Console.WriteLine("5) просмотреть каталог;");
                Console.WriteLine("6) просмотреть диск;");
                Console.WriteLine("7) поиск по исполнителю;");
                Console.WriteLine("8) выход;");

                Console.WriteLine("\nВыберите действие:");
                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1:
                        AddDisk(catalog);
                        break;
                    case 2:
                        DeleteDisk(catalog);
                        break;
                    case 3:
                        AddSong(catalog);
                        break;
                    case 4:
                        DeleteSong(catalog);
                        break;
                    case 5:
                        PrintCatalog(catalog);
                        break;
                    case 6:
                        PrintDisk(catalog);
                        break;
                    case 7:
                        SearchSongs(catalog);
                        break;
                    case 8:
                        Console.WriteLine("\nВыход из программы");
                        break;
                }
            }
        }

        static void AddDisk(Hashtable catalog)
        {
            Console.WriteLine("Введите название диска:");
            string name = Console.ReadLine();
            Console.WriteLine("Введите имя исполнителя:");
            string author = Console.ReadLine();

            catalog.Add(GetNextNumber(catalog), new Disk() { Name = name, Author = author });
            Console.WriteLine("Диск добавлен");
            Console.ReadLine();
        }

        static void DeleteDisk(Hashtable catalog)
        {
            Console.WriteLine("Введите номер диска:");
            int number = -1;
            if (int.TryParse(Console.ReadLine(), out number) && catalog.ContainsKey(number))
            {
                catalog.Remove(number);
                Console.WriteLine("Диск удалён");
            }
            else
                Console.WriteLine("Диск с таким номером отсутствует");
            Console.ReadLine();
        }

        static void AddSong(Hashtable catalog)
        {
            Console.WriteLine("Введите номер диска:");
            int number = -1;
            if (int.TryParse(Console.ReadLine(), out number) && catalog.ContainsKey(number))
            {
                Disk disk = catalog[number] as Disk;
                Console.WriteLine("Введите название песни:");
                disk.Songs.Add(GetNextNumber(disk.Songs), Console.ReadLine());
                Console.WriteLine("Песня добавлена");
            }
            else
                Console.WriteLine("Диск с таким номером отсутствует");
            Console.ReadLine();
        }

        static void DeleteSong(Hashtable catalog)
        {
            Console.WriteLine("Введите номер диска:");
            int number = -1;
            if (int.TryParse(Console.ReadLine(), out number) && catalog.ContainsKey(number))
            {
                Disk disk = catalog[number] as Disk;
                Console.WriteLine("Введите номер песни:");
                if (int.TryParse(Console.ReadLine(), out number) && disk.Songs.ContainsKey(number))
                {
                    disk.Songs.Remove(number);
                    Console.WriteLine("Песня удалена");
                }
                else
                    Console.WriteLine("Песня с таким номером отсутствует");
            }
            else
                Console.WriteLine("Диск с таким номером отсутствует");
            Console.ReadLine();
        }

        static void PrintCatalog(Hashtable catalog)
        {
            if (catalog.Count == 0)
                Console.WriteLine("\nКаталог пуст");
            else
            {
                Console.WriteLine("\nКАТАЛОГ:");
                foreach (var key in catalog.Keys.Cast<int>().OrderBy(k => k))
                {
                    Disk disk = catalog[key] as Disk;
                    Console.WriteLine("{0}. {1}, {2}", key, disk.Name, disk.Author);
                    foreach (var skey in disk.Songs.Keys.Cast<int>().OrderBy(k => k))
                        Console.WriteLine("\t{0}) {1}", skey, disk.Songs[skey]);
                }
            }
            Console.ReadLine();
        }

        static void PrintDisk(Hashtable catalog)
        {
            Console.WriteLine("Введите номер диска:");
            int number = -1;
            if (int.TryParse(Console.ReadLine(), out number) && catalog.ContainsKey(number))
            {
                Disk disk = catalog[number] as Disk;
                Console.WriteLine("{0}, {1}", disk.Name, disk.Author);
                foreach (var skey in disk.Songs.Keys.Cast<int>().OrderBy(k => k))
                    Console.WriteLine("\t{0}) {1}", skey, disk.Songs[skey]);
            }
            else
                Console.WriteLine("Диск с таким номером отсутствует");
            Console.ReadLine();
        }

        static void SearchSongs(Hashtable catalog)
        {
            Console.WriteLine("Введите имя исполнителя:");
            string author = Console.ReadLine();

            Console.WriteLine("Песни:");
            foreach (var key in catalog.Keys.Cast<int>().OrderBy(k => k))
            {
                Disk disk = catalog[key] as Disk;
                if (disk.Author == author)
                    foreach (var skey in disk.Songs.Keys.Cast<int>().OrderBy(k => k))
                        Console.WriteLine(" {0}", disk.Songs[skey]);
            }
            Console.ReadLine();
        }

        static void FillCatalogWithDefaultAlbums(Hashtable catalog)
        {
            Disk disk2 = new Disk() { Name = "Disk2", Author = "Author2" };
            disk2.Songs.Add(3, "Song3 A2");
            disk2.Songs.Add(2, "Song2 A2");
            disk2.Songs.Add(1, "Song1 A2");
            catalog.Add(2, disk2);

            Disk disk1 = new Disk() { Name = "Disk1", Author = "Author1" };
            disk1.Songs.Add(1, "Song1 A1");
            disk1.Songs.Add(2, "Song2 A1");
            disk1.Songs.Add(3, "Song3 A1");
            catalog.Add(1, disk1);
        }

        static int GetNextNumber(Hashtable table)
        {
            int retval = 1;
            while (true)
            {
                if (!table.ContainsKey(retval))
                    break;
                retval++;
            }
            return retval;
        }
    }

    public class Disk
    {
        private Hashtable _songs = new Hashtable();

        public string Name { get; set; }

        public string Author { get; set; }

        public Hashtable Songs { get { return _songs; } }
    }
}
