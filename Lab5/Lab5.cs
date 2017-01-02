using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab5
{
    /// <summary>
    /// 5.	На основе данных входного файла составить список сотрудников учреждения, 
    /// включив следующие данные: ФИО, год принятия на работу, должность, зарплата, рабочий стаж. 
    /// Вывести в новый файл информацию о сотрудниках, имеющих зарплату ниже определенного уровня, 
    /// отсортировав их по рабочему стажу. 
    /// </summary>
    class Lab5
    {
        const string INPUT_PATH = "input.txt";
        const string OUTPUT_PATH = "output.txt";

        static void Main(string[] args)
        {
            try
            {
                Employee[] employees = ReadInput().ToArray();
                Console.WriteLine("ИСХОДНЫЙ МАССИВ: ");
                PrintEmployees(employees);

                Console.WriteLine("\nВведите уровень зарплаты: ");
                int salaryRate = int.Parse(Console.ReadLine());
                employees = employees.Where(e => e.Salary < salaryRate).ToArray();
                Console.WriteLine("\nОТФИЛЬТРОВАННЫЙ МАССИВ: ");
                PrintEmployees(employees);

                Array.Sort(employees);
                Console.WriteLine("\nОТСОРТИРОВАННЫЙ МАССИВ: ");
                PrintEmployees(employees);

                var output = employees.Select<Employee, string>(e => e.ToString());              
                File.WriteAllLines(OUTPUT_PATH, output.ToArray());
                Console.WriteLine("\nФАЙЛ СОХРАНЁН");

                //StreamWriter f = new StreamWriter(new FileStream("output.txt", FileMode.Create, FileAccess.Write));//создаем для записи
                //f.WriteLine("");
                //f.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();
        }

        static List<Employee> ReadInput()
        {
            List<Employee> retval = new List<Employee>();
            foreach (var line in File.ReadLines(INPUT_PATH, Encoding.GetEncoding(1251)))
            {
                string[] data = line.Split(',');
                Employee employee = new Employee(
                    data[0].Trim(),     // name;
                    int.Parse(data[1]), // entryYear;
                    data[2].Trim(),     // position;
                    int.Parse(data[3]), // salary;
                    int.Parse(data[4])  // workExperience
                );
                retval.Add(employee);
            }
            return retval;

            //StreamReader fileIn = new StreamReader("input.txt", Encoding.GetEncoding(1251));
            //string line;
            //while ((line = fileIn.ReadLine()) != null) { }
            //fileIn.Close();           
        }

        static void PrintEmployees(Employee[] employees)
        {
            foreach (var item in employees)
                Console.WriteLine(item);
        }

    }
}
