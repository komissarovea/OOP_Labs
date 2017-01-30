using Samost.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Samost
{
    /// <summary>
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
    class Program
    {
        #region Constant Fields

        const string DEPOSITS_FILE = "deposits.xml";
        const string CLIENTS_FILE = "clients.xml";

        #endregion

        #region Deposit Types

        /// <summary>
        /// Загрузить типы вкладов (описание процентов)
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, DepositType> LoadDepositTypes()
        {
            Dictionary<int, DepositType> depositTypes = new Dictionary<int, DepositType>();
            if (File.Exists(DEPOSITS_FILE))
            {
                XDocument xdoc = XDocument.Load(DEPOSITS_FILE);
                foreach (var node in xdoc.Root.Elements())
                {
                    DepositType type = new DepositType()
                    {
                        Key = Convert.ToInt32(node.Attribute("key").Value),
                        Name = node.Attribute("name").Value,
                        Percent = Convert.ToInt32(node.Attribute("percent").Value)
                    };
                    depositTypes.Add(type.Key, type);
                }
            }
            else
            {
                File.WriteAllText(DEPOSITS_FILE, Properties.Resources.deposits);
                depositTypes = LoadDepositTypes();
            }
            return depositTypes;
        }

        /// <summary>
        /// Отобразить типы вкладов 
        /// </summary>
        /// <param name="depositTypes"></param>
        public static void ShowDepositTypes(Dictionary<int, DepositType> depositTypes)
        {
            Console.WriteLine("Информация о процентах по вкладам:");
            foreach (var key in depositTypes.Keys)
                Console.WriteLine("{0}) {1}", key, depositTypes[key]);
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
            List<Client> clients = new List<Client>();
            if (File.Exists(CLIENTS_FILE))
            {
                XDocument xdoc = XDocument.Load(CLIENTS_FILE);
                foreach (var clientNode in xdoc.Root.Elements())
                {
                    Client client = new Client()
                    {
                        Name = clientNode.Attribute("name").Value,
                        PercentBonus = Convert.ToInt32(clientNode.Attribute("percentBonus").Value),
                        FixBonus = Convert.ToInt32(clientNode.Attribute("fixBonus").Value)
                    };
                    foreach (var depositNode in clientNode.Elements())
                    {
                        Deposit deposit = new Deposit()
                        {
                            //InitialDate = Convert.ToDateTime(depositNode.Attribute("initialDate").Value),
                            Type = depositTypes[Convert.ToInt32(depositNode.Attribute("type").Value)]
                        };
                        foreach (var paymentNode in depositNode.Elements())
                        {
                            Payment payment = new Payment()
                            {
                                Date = Convert.ToDateTime(paymentNode.Attribute("date").Value),
                                Sum = Convert.ToInt32(paymentNode.Attribute("sum").Value)
                            };
                            deposit.Payments.Add(payment);
                        }
                        client.Deposits.Add(deposit);
                    }
                    clients.Add(client);
                }
            }
            else
            {
                File.WriteAllText(CLIENTS_FILE, Properties.Resources.clients);
                clients = LoadClients(depositTypes);
            }
            return clients;
        }

        /// <summary>
        /// Сохранить информацию о клиентах
        /// </summary>
        /// <param name="clients"></param>
        public static void SaveClients(List<Client> clients)
        {
            XDocument xdoc = new XDocument(new XElement("root"));
            foreach (var client in clients)
            {
                var clientNode = new XElement("client",
                    new XAttribute("name", client.Name),
                    new XAttribute("percentBonus", client.PercentBonus),
                    new XAttribute("fixBonus", client.FixBonus));
                foreach (var deposit in client.Deposits)
                {
                    var depositNode = new XElement("deposit",
                        new XAttribute("type", deposit.Type.Key));
                    foreach (var payment in deposit.Payments)
                    {
                        var paymentNode = new XElement("payment",
                            new XAttribute("date", payment.Date.ToShortDateString()),
                            new XAttribute("sum", payment.Sum));
                        depositNode.Add(paymentNode);
                    }
                    clientNode.Add(depositNode);
                }
                xdoc.Root.Add(clientNode);
            }
            xdoc.Save(CLIENTS_FILE);
        }

        /// <summary>
        /// Отобразить информацию о клиентах
        /// </summary>
        /// <param name="clients"></param>
        public static void ShowClients(List<Client> clients)
        {
            Console.WriteLine("Информация о клиентах:");
            for (int i = 1; i <= clients.Count; i++)
            {
                var client = clients[i - 1];
                Console.WriteLine("{0}. {1}", i, client);
                Console.WriteLine("Вклады:");
                for (int j = 1; j <= client.Deposits.Count; j++)
                    Console.WriteLine("\t{0}) {1}", j, client.Deposits[j - 1]);
                Console.WriteLine();
            }
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
            int x = 0;
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out x))
                    break;
                Console.WriteLine("Введите целое число: ");
            }
            return x;
        }

        /// <summary>
        /// Считать с консоли положительное целочисленное значение
        /// </summary>
        /// <returns></returns>
        public static int ReadPositiveInt()
        {
            int x = 0;
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out x) && x > 0)
                    break;
                Console.WriteLine("Введите целое положительное число: ");
            }
            return x;
        }

        #endregion

        /// <summary>
        /// Точка входа в программу
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                Dictionary<int, DepositType> depositTypes = LoadDepositTypes();
                List<Client> clients = LoadClients(depositTypes);

                int choice = -1;
                while (choice != 5)
                {
                    Console.Clear();
                    Console.WriteLine("МЕНЮ:");
                    Console.WriteLine("1) просмотреть информацию о процентах по вкладам;");
                    Console.WriteLine("2) просмотреть информацию о клиентах;");
                    Console.WriteLine("3) пополнить величину вклада;");
                    Console.WriteLine("4) вычислить общую сумму выплат для всех вкладов;");
                    Console.WriteLine("5) сохранить и выйти;");
                    Console.WriteLine("\nВыберите действие:");
                    choice = ReadInt();
                    switch (choice)
                    {
                        case 1:
                            ShowDepositTypes(depositTypes);
                            break;
                        case 2:
                            ShowClients(clients);
                            break;
                        case 3:
                            ReplenishDeposit(clients);
                            break;
                        case 4:
                            CalculateTotalPayoutSum(clients);
                            break;
                        case 5:
                            SaveClients(clients);
                            break;
                    }
                    Console.Write(choice != 5 ? "\nВернуться в меню... (Enter)" : "Данные сохранены. Выйти... (Enter)");
                    Console.ReadLine();
                }
            }
            catch (NameException nex)
            {
                Console.WriteLine(nex.Message);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }
    }
}
