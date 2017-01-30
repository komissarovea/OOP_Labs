using Samost.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Samost
{
    class Misc
    {
        #region Constant Fields

        const string DEPOSITS_FILE = "deposits.xml";
        const string CLIENTS_FILE = "clients.xml";

        #endregion

        /// <summary>
        /// Сохранить типы вкладов
        /// </summary>
        /// <param name="depositTypes"></param>
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

        public static int GetNextNumber(IDictionary dict)
        {
            int retval = 1;
            while (true)
            {
                if (!dict.Contains(retval))
                    break;
                retval++;
            }
            return retval;
        }

        public static void AddDepositType(List<DepositType> depositTypes)
        {
            Console.WriteLine("Введите название вклада:");
            string name = Console.ReadLine();
            Console.WriteLine("Введите процент по вкладу:");
            int percent = StaticHelper.ReadPositiveInt();

            depositTypes.Add(new DepositType() { Name = name, Percent = percent });
            Console.WriteLine("Вклад добавлен");
            Console.ReadLine();
        }

        public static void DeleteDepositType(List<DepositType> depositTypes)
        {
            //ShowDepositTypes(depositTypes);
            Console.WriteLine("\nВведите номер вклада:");
            int number = StaticHelper.ReadPositiveInt();
            if (number <= depositTypes.Count)
            {
                depositTypes.RemoveAt(number - 1);
                Console.WriteLine("Вклад удалён");
            }
            else
                Console.WriteLine("Вклад с таким номером отсутствует");
            Console.ReadLine();
        }

        public static void AddClient(List<Client> clients)
        {

        }

        public static void DeleteClient(List<Client> clients)
        {

        }

        static void Main2(string[] args)
        {
            try
            {
                Dictionary<int, DepositType> depositTypes = StaticHelper.LoadDepositTypes();
                List<Client> clients = StaticHelper.LoadClients(depositTypes);

                int choice = -1;
                while (choice != 9)
                {
                    Console.Clear();
                    Console.WriteLine("МЕНЮ:");
                    //Console.WriteLine("1) добавить тип вклада;");
                    //Console.WriteLine("2) удалить тип вклада;");
                    Console.WriteLine("3) посмотреть информацию о процентах по вкладам;");
                    Console.WriteLine("4) добавить клиента;");
                    Console.WriteLine("5) удалить клиента;");
                    Console.WriteLine("6) просмотреть список клиентов;");
                    Console.WriteLine("7) пополнить величину вклада;");
                    Console.WriteLine("8) вычислить общую сумму выплат для всех вкладов;");
                    Console.WriteLine("9) сохранить и выйти;");

                    Console.WriteLine("\nВыберите действие:");
                    //int.TryParse(Console.ReadLine(), out choice);
                    choice = StaticHelper.ReadInt();

                    switch (choice)
                    {
                        case 1:
                            //StaticHelper.AddDepositType(depositTypes);
                            break;
                        case 2:
                            //StaticHelper.DeleteDepositType(depositTypes);
                            break;
                        case 3:
                            StaticHelper.ShowDepositTypes(depositTypes);
                            break;
                        case 4:
                            //StaticHelper.AddClient(clients);
                            break;
                        case 5:
                            //StaticHelper.DeleteClient(clients);
                            break;
                        case 6:
                            StaticHelper.ShowClients(clients);
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            StaticHelper.SaveDepositTypes(depositTypes);
                            StaticHelper.SaveClients(clients);
                            Console.WriteLine("Данные сохранены.");
                            break;
                    }
                }
            }
            catch (NameException nex)
            {
                Console.WriteLine(nex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();
        }

    }
}
