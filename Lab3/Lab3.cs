using System;
using System.IO;
using System.Xml.Serialization;

namespace Lab3
{
    /// <summary>
    ///  Вариант 5
    ///     1.	Создать абстрактный класс Function с методом вычисления значения функции y = f(x) в заданной точке.
    ///     2.	Создать производные классы: Line(y= кx + b), Kub(y= kx2 + bx + c), Parabola(y= кx2 + c) со своими методами вычисления значения в заданной точке.
    ///     3.	Создать массив n функций и вывести полную информацию о значении данных функций в точке х.
    /// </summary>
    class Lab3
    {
        const string FILE_NAME = "data.xml";

        static void Main(string[] args)
        {
            try
            {
                const int x = 2;
                Function[] arr = new Function[]
                {
                    new Line(2, 5),
                    new Kub(6, 4, 7),
                    new Parabola(3, 8),
                    new Line(9, 1)
                };
                XmlSerializer serializer = new XmlSerializer(typeof(Function[]));
                //StringWriter writer = new StringWriter();
                //serializer.Serialize(writer, arr);
                //string xml = writer.ToString();
                //File.WriteAllText(FILE_NAME, xml);

                string xml2 = File.ReadAllText(FILE_NAME);
                StringReader reader = new StringReader(xml2);
                Function[] arr2 = serializer.Deserialize(reader) as Function[];
                Console.WriteLine("x = {0}\n", x);
                foreach (Function function in arr2)
                {
                    int y = function.Calculate(x);
                    Console.WriteLine("{0}, значение функции: {1}", function, y);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }
    }
}
