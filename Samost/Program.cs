using Samost.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samost
{
    /// <summary>
    /// Предметная область: Банк. Информационная система банка хранит описание процентов по различным вкладам. 
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
        static void Main(string[] args)
        {
            try
            {
                Dictionary<int, DepositType> depositTypes = StaticHelper.LoadDepositTypes();
                List<Client> clients = StaticHelper.LoadClients(depositTypes);

                int choice = -1;
                while (choice != 5)
                {
                    Console.Clear();
                    Console.WriteLine("МЕНЮ:");
                    Console.WriteLine("1) посмотреть информацию о процентах по вкладам;");
                    Console.WriteLine("2) просмотреть информацию о клиентах;");
                    Console.WriteLine("3) пополнить величину вклада;");
                    Console.WriteLine("4) вычислить общую сумму выплат для всех вкладов;");
                    Console.WriteLine("5) сохранить и выйти;");

                    Console.WriteLine("\nВыберите действие:");
                    //int.TryParse(Console.ReadLine(), out choice);
                    choice = StaticHelper.ReadInt();

                    switch (choice)
                    {
                        case 1:
                            StaticHelper.ShowDepositTypes(depositTypes);
                            break;
                        case 2:
                            StaticHelper.ShowClients(clients);
                            break;
                        case 3:
                            StaticHelper.ReplenishDeposit(clients);
                            break;
                        case 4:
                            StaticHelper.CalculateTotalPayoutSum(clients);
                            break;
                        case 5:
                            StaticHelper.SaveDepositTypes(depositTypes);
                            StaticHelper.SaveClients(clients);
                            Console.WriteLine("Данные сохранены.");
                            Console.Write("Выйти...");
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
