using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    class Lab1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вариант 5");
            try
            {
                //Point1.Run();             
                Point1TwoDimensional.Run();
                //Point2.Run();
                //Point3.Run();
                //Point4.Run();
                //Point5.Run();
                //Point6.Run();

                //Samost1.Run();
                Samost2.Run();
                //Samost3.Run();
                //Samost4.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.ReadLine();
        }
    }
}
