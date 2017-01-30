using Samost.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Samost
{
    public static class StaticHelper
    {
        const string DEPOSITS_FILE = "deposits.xml";
        const string CLIENTS_FILE = "clients.xml";

        #region Deposit Types

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

        public static void SaveDepositTypes(Dictionary<int, DepositType> depositTypes)
        {
            XDocument xdoc = new XDocument(new XElement("root"));
            foreach (var key in depositTypes.Keys)
            {
                xdoc.Root.Add(new XElement("deposit",
                    new XAttribute("key", key),
                    new XAttribute("name", depositTypes[key].Name),
                    new XAttribute("percent", depositTypes[key].Percent)
                    ));
            }
            xdoc.Save(DEPOSITS_FILE);
        }

        public static void ShowDepositTypes(Dictionary<int, DepositType> depositTypes)
        {
            Console.WriteLine("Информация о процентах по вкладам:");
            foreach (var key in depositTypes.Keys)
                Console.WriteLine("{0}) {1}", key, depositTypes[key]);
        }

        #endregion

        #region Clients

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

        public static void ReplenishDeposit(List<Client> clients)
        {
            Console.WriteLine("Клиенты:");
            for (int i = 1; i <= clients.Count; i++)
                Console.WriteLine("{0}. {1}", i, clients[i - 1].Name);
            Console.WriteLine("Введите номер клиента:");
            int clientNumber = StaticHelper.ReadPositiveInt();
            if (clientNumber <= clients.Count)
            {
                var client = clients[clientNumber - 1];
                Console.WriteLine("Вклады:");
                for (int j = 1; j <= client.Deposits.Count; j++)
                    Console.WriteLine("{0}) {1}", j, client.Deposits[j - 1]);
                Console.WriteLine("Введите номер вклада:");
                int depositNumber = StaticHelper.ReadPositiveInt();
                if (depositNumber <= client.Deposits.Count)
                {
                    var deposit = client.Deposits[depositNumber - 1];
                    Console.WriteLine("Введите сумму:");
                    int paymentSum = StaticHelper.ReadInt();
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

        public static int GetDaysInYear(DateTime thisDate)
        {
            var nextDate = thisDate.AddYears(1);
            return (nextDate - thisDate).Days;
        }

        #endregion
    }
}
