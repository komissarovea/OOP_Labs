/// <summary>
/// Комиссаров Евгений Александрович, 50326-2, вариант 5
/// 
/// Предметная область: Банк. 
/// Информационная система банка хранит описание процентов по различным вкладам. 
/// Система хранит информацию о вкладчиках и сделанных ими вкладах. 
/// Каждый вкладчик имеет дополнительные бонусы.
/// Одним вкладчикам бонусы заданы в процентах от вклада, 
/// другие имеют бонусы в виде фиксированной суммы к вкладу.
/// 
/// Система должна позволять выполнять следующие задачи:
/// •	хранить информацию о процентах по вкладам;
/// •	хранить информацию о клиентах;
/// •	пополнять клиенту величину вклада;
/// •	вычислять общую сумму выплат для всех вкладов.
/// 
/// Добавить обработку исключительных ситуаций:
/// •	величина вклада отрицательна
/// •	имя вкладчика меньше двух букв.
/// 
/// Добавить перегруженный унарный оператор для увеличения величины вклада.
/// </summary>

using System; //подключение общей библиотеки классов
using System.Collections.Generic; //подключение библиотеки классов, определяющих типизированные коллекции
using System.IO; //подключение библиотеки с набором классов для работы с файлами
using System.Xml.Linq; //подключение библиотеки для работы с xml-документами

namespace Samost //пространство имён
{
    /// <summary>
    /// Основной класс программы
    /// </summary>
    class Program
    {
        // начало выделенного участка кода (для читабельности, можно сворачивать)
        #region Constant Fields 

        const string DEPOSITS_FILE = "deposits.xml"; // путь к файлу с типами вкладов
        const string CLIENTS_FILE = "clients.xml"; // путь к файлу со списком клиентов

        // конец выделенного участка кода
        #endregion

        #region Properties

        /// <summary>
        /// Типы вкладов
        /// </summary>
        public static Dictionary<int, DepositType> DepositTypes { get; private set; } // задать или считать значение

        /// <summary>
        /// Список клиентов
        /// </summary>
        public static List<Client> Clients { get; private set; } // задать или считать значение

        #endregion

        #region Deposit Types

        /// <summary>
        /// Загрузить типы вкладов (описание процентов)
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, DepositType> LoadDepositTypes()
        {
            // создание коллекции для хранения типов вкладов
            Dictionary<int, DepositType> depositTypes = new Dictionary<int, DepositType>(); 

            if (!File.Exists(DEPOSITS_FILE)) // проверка существования файла
                File.WriteAllText(DEPOSITS_FILE, Properties.Resources.deposits); // создание файла с данными по умолчанию

            XDocument xdoc = XDocument.Load(DEPOSITS_FILE); // загрузка файла и создание объекта XDocument
            foreach (var node in xdoc.Root.Elements()) // перебор дочерних узлов
            {
                DepositType type = new DepositType() // создание объекта DepositType
                {
                    Key = Convert.ToInt32(node.Attribute("key").Value), // задание свойств используя значения атрибутов
                    Name = node.Attribute("name").Value,                // с конвертацией в целочисленное значение при необходимости    
                    Percent = Convert.ToInt32(node.Attribute("percent").Value)
                };
                depositTypes.Add(type.Key, type); // добавление объекта в depositTypes, в качестве ключа выступает идентификатор типа вклада
            }

            return depositTypes; // вернуть depositTypes
        }

        /// <summary>
        /// Отобразить типы вкладов 
        /// </summary>
        /// <param name="depositTypes"></param>
        public static void ShowDepositTypes(Dictionary<int, DepositType> depositTypes)
        {
            if (depositTypes != null && depositTypes.Count > 0) // проверка коллекции на наличие типов вкладов
            {
                Console.WriteLine("Информация о процентах по вкладам:"); // вывод на консоль
                foreach (var key in depositTypes.Keys) // перебор ключей коллекции
                    Console.WriteLine("{0}) {1}", key, depositTypes[key]); // вывод на консоль информации о типе вклада
            }
            else // в случае отсутствия элементов
                Console.WriteLine("Информация о процентах по вкладам отсутствует"); // вывод на консоль
        }

        #endregion

        #region Clients

        /// <summary>
        /// Загрузить информацию о клиентах
        /// </summary>
        /// <param name="depositTypes"></param>
        /// <returns></returns>
        public static List<Client> LoadClients(Dictionary<int, DepositType> depositTypes)
        {
            List<Client> clients = new List<Client>(); // создание коллекции для хранения списка клиентов
            if (!File.Exists(CLIENTS_FILE)) // проверка существования файла
                File.WriteAllText(CLIENTS_FILE, Properties.Resources.clients); // создание файла с данными по умолчанию

            XDocument xdoc = XDocument.Load(CLIENTS_FILE); // загрузка файла и создание объекта XDocument
            foreach (var clientNode in xdoc.Root.Elements()) // перебор дочерних узлов
            {
                Client client = new Client() // создание объекта Client
                {
                    Name = clientNode.Attribute("name").Value, // задание свойств используя значения атрибутов
                    PercentBonus = Convert.ToInt32(clientNode.Attribute("percentBonus").Value), // с конвертацией в целочисленное значение при необходимости
                    FixBonus = Convert.ToInt32(clientNode.Attribute("fixBonus").Value)
                };
                foreach (var depositNode in clientNode.Elements()) // перебор дочерних узлов clientNode
                {
                    Deposit deposit = new Deposit() // создание объекта Deposit
                    {
                        Type = depositTypes[Convert.ToInt32(depositNode.Attribute("type").Value)] // задание свойства используя значения атрибутов
                    };
                    foreach (var paymentNode in depositNode.Elements()) // перебор дочерних узлов depositNode
                    {
                        Payment payment = new Payment() // создание объекта Payment
                        {
                            Date = Convert.ToDateTime(paymentNode.Attribute("date").Value), // задание свойства используя значения атрибутов
                            Sum = Convert.ToInt32(paymentNode.Attribute("sum").Value) // с конвертацией в целочисленное значение
                        };
                        deposit.Payments.Add(payment); // добавление объекта payment в deposit.Payments
                    }
                    client.Deposits.Add(deposit); // добавление объекта deposit в client.Deposits
                }
                clients.Add(client); // добавление объекта client в clients
            }
            return clients; // вернуть clients
        }

        /// <summary>
        /// Сохранить информацию о клиентах
        /// </summary>
        /// <param name="clients"></param>
        public static void SaveClients(List<Client> clients)
        {
            XDocument xdoc = new XDocument(new XElement("root")); // создание объекта XDocument с корневым элементом root
            foreach (var client in clients) // перебор элементов коллекции clients
            {
                var clientNode = new XElement("client", // создание нового узла client
                    new XAttribute("name", client.Name), // создание атрибута name
                    new XAttribute("percentBonus", client.PercentBonus), // создание атрибута percentBonus
                    new XAttribute("fixBonus", client.FixBonus)); // создание атрибута fixBonus
                foreach (var deposit in client.Deposits) // перебор элементов коллекции client.Deposits
                {
                    var depositNode = new XElement("deposit", // создание нового узла deposit
                        new XAttribute("type", deposit.Type.Key)); // создание атрибута type
                    foreach (var payment in deposit.Payments)
                    {
                        var paymentNode = new XElement("payment", // создание нового узла payment
                            new XAttribute("date", payment.Date.ToShortDateString()), // создание атрибута date
                            new XAttribute("sum", payment.Sum)); // создание атрибута sum
                        depositNode.Add(paymentNode); // добавление узла paymentNode в узел depositNode
                    }
                    clientNode.Add(depositNode); // добавление узла depositNode в узел clientNode
                }
                xdoc.Root.Add(clientNode); // добавление узла clientNode в корневой узел
            }
            xdoc.Save(CLIENTS_FILE); // сохранение xml-документа в файл
        }

        /// <summary>
        /// Отобразить информацию о клиентах
        /// </summary>
        /// <param name="clients"></param>
        public static void ShowClients(List<Client> clients)
        {
            if (clients != null && clients.Count > 0) // проверка коллекции на наличие клиентов
            {
                Console.WriteLine("Информация о клиентах:"); // вывод на консоль
                for (int i = 1; i <= clients.Count; i++) // перебор списка клиентов
                {
                    var client = clients[i - 1]; // инициализация переменной client
                    Console.WriteLine("{0}. {1}", i, client); // вывод на консоль информации о клиенте
                    Console.WriteLine("Вклады:"); // вывод на консоль
                    for (int j = 1; j <= client.Deposits.Count; j++) // перебор вкладов клиента
                        Console.WriteLine("\t{0}) {1}", j, client.Deposits[j - 1]); // вывод на консоль информации о вкладе
                    Console.WriteLine(); // вывод на консоль пустой строки
                }
            }
            else // в случае отсутствия элементов
                Console.WriteLine("Информация о клиентах отсутствует"); // вывод на консоль
        }

        #endregion

        #region Deposits

        /// <summary>
        /// Пополнить вклад одного из клиентов
        /// </summary>
        /// <param name="clients"></param>
        public static void ReplenishDeposit(List<Client> clients)
        {
            Console.WriteLine("Клиенты:");
            for (int i = 1; i <= clients.Count; i++)
                Console.WriteLine("{0}. {1}", i, clients[i - 1].Name);
            Console.WriteLine("Введите номер клиента:");
            int clientNumber = ReadPositiveInt();
            if (clientNumber <= clients.Count)
            {
                var client = clients[clientNumber - 1];
                Console.WriteLine("Вклады:");
                for (int j = 1; j <= client.Deposits.Count; j++)
                    Console.WriteLine("{0}) {1}", j, client.Deposits[j - 1]);
                Console.WriteLine("Введите номер вклада:");
                int depositNumber = ReadPositiveInt();
                if (depositNumber <= client.Deposits.Count)
                {
                    var deposit = client.Deposits[depositNumber - 1];
                    Console.WriteLine("Введите сумму:");
                    int paymentSum = ReadInt();
                    try
                    {
                        deposit.Replenish(paymentSum);
                        Console.WriteLine("Сумма добавлена к вкладу!");
                    }
                    catch (DepositException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                    Console.WriteLine("Вклада с таким номером нет!");
            }
            else
                Console.WriteLine("Клиента с таким номером нет!");
        }

        /// <summary>
        /// Вычислить общую сумму выплат для всех вкладов
        /// </summary>
        /// <param name="clients"></param>
        public static void CalculateTotalPayoutSum(List<Client> clients)
        {
            int totalSum = 0;
            Console.WriteLine("Выплаты:");
            foreach (var client in clients)
            {
                int clientSum = client.GetPayoutSum();
                Console.WriteLine("{0} - {1} руб.", client.Name, clientSum);
                totalSum += clientSum;
            }
            Console.WriteLine("\nОбщая сумма выплат: {0} руб.", totalSum);
        }

        #endregion

        #region Misc

        /// <summary>
        /// Считать с консоли целочисленное значение
        /// </summary>
        /// <returns></returns>
        public static int ReadInt()
        {
            int x = 0; // инициализация переменной x
            while (true) // бесконечный цикл
            {
                if (Int32.TryParse(Console.ReadLine(), out x)) // считать значение с консоли и конвертировать в число с проверкой
                    break; // прервать цикл
                Console.WriteLine("Введите целое число: "); // вывод на консоль
            }
            return x; // вернуть x
        }

        /// <summary>
        /// Считать с консоли положительное целочисленное значение
        /// </summary>
        /// <returns></returns>
        public static int ReadPositiveInt()
        {
            int x = 0; // инициализация переменной x
            while (true) // бесконечный цикл
            {
                if (Int32.TryParse(Console.ReadLine(), out x) && x > 0) // считать значение с консоли и конвертировать в число с проверкой
                    break; // прервать цикл
                Console.WriteLine("Введите целое положительное число: "); // вывод на консоль
            }
            return x; // вернуть x
        }

        #endregion

        /// <summary>
        /// Точка входа в программу
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try // Обработка исключений
            {
                DepositTypes = LoadDepositTypes(); // Загрузить типы вкладов (описание процентов)
                Clients = LoadClients(DepositTypes); // Загрузить информацию о клиентах

                int menuItem = 0; // инициализация переменной menuItem
                while (menuItem != 5) // цикл с проверкой значения
                {
                    Console.Clear(); // очистка консоли
                    Console.WriteLine("МЕНЮ:"); // вывод на консоль
                    Console.WriteLine("1) просмотреть информацию о процентах по вкладам;"); 
                    Console.WriteLine("2) просмотреть информацию о клиентах;");
                    Console.WriteLine("3) пополнить величину вклада;");
                    Console.WriteLine("4) вычислить общую сумму выплат для всех вкладов;");
                    Console.WriteLine("5) сохранить и выйти;");
                    Console.WriteLine("\nВыберите действие:");
                    menuItem = ReadInt(); // Считать с консоли целочисленное значение
                    switch (menuItem) // проверка значения переменной
                    {
                        case 1: // если значение равно 1
                            ShowDepositTypes(DepositTypes); // Отобразить типы вкладов 
                            break;
                        case 2:
                            ShowClients(Clients); // Отобразить информацию о клиентах
                            break;
                        case 3:
                            ReplenishDeposit(Clients); // Пополнить вклад одного из клиентов
                            break;
                        case 4:
                            CalculateTotalPayoutSum(Clients); // Вычислить общую сумму выплат для всех вкладов
                            break;
                        case 5:
                            SaveClients(Clients); // Сохранить информацию о клиентах
                            break;
                    }
                    Console.Write(menuItem != 5 ? "\nВернуться в меню... (Enter)" : "Данные сохранены. Выйти... (Enter)"); // вывод на консоль
                    Console.ReadLine(); // Считать с консоли перевод строки
                }
            }
            catch (NameException nex) // Обработка ошибки именования
            {
                Console.WriteLine(nex.Message); // вывод на консоль текста ошибки
                Console.ReadLine(); // Считать с консоли перевод строки
            }
            catch (Exception ex) // Обработка прочих ошибок
            {
                Console.WriteLine(ex.ToString()); // вывод на консоль информации об ошибке
                Console.ReadLine(); // Считать с консоли перевод строки
            }
        }
    }
}
