using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab2
{
    /// <summary>
    /// Комиссаров Е.А., 5 вариант
    ///     5.	Создать класс для работы с одномерным массивом целых чисел. Разработать следующие элементы класса: 
    ///     o Поля: 
    ///     	int[] IntArray; 
    ///     	int n.
    ///     o   Конструктор, позволяющий создать массив размерности n.
    ///     o Методы, позволяющие: 
    ///     	ввести элементы массива с клавиатуры; 
    ///     	вывести элементы массива на экран; 
    ///     	отсортировать элементы массива в порядке возрастания.
    ///     o   Свойства: 
    ///     	возвращающее размерность массива (доступное только для чтения); 
    ///     	позволяющее помножить все элементы массива на скаляр(доступное только для записи). 
    /// 
    ///     5.	Добавить в класс для работы с одномерным массивом целых чисел: 
    ///     o Индексатор, позволяющий по индексу обращаться к соответствующему элементу массива.
    ///     o Перегрузку: 
    ///         	операции ++ (--): одновременно увеличивает(уменьшает) значение всех элементов массива на 1; 
    ///         	операции !: возвращает значение true, если элементы массива не упорядочены по возрастанию, иначе false; 
    ///         	операции бинарный *: домножить все элементы массива на скаляр; 
    ///         	преобразования класса массив в одномерный массив(и наоборот). 
    /// </summary>
    class Lab2
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вариант 5");
            try
            {
                Console.WriteLine("Введите размер массива:");
                int size = ReadPositiveInt();
                OneDimensionArray array = new OneDimensionArray(size);
                array.Input();

                Console.Write("Исходный массив: ");
                array.Print();

                Console.Write("Массив упорядочен: {0}", array.Sorted);

                array.SortAsc();
                Console.Write("Отсортированный массив: ");
                array.Print();

                Console.WriteLine("Массив упорядочен: {0}", array.Sorted);               

                OneDimensionArray array1 = array.Copy();
                array1++;
                Console.Write("Оператор ++:");
                array1.Print();

                OneDimensionArray array2 = array.Copy();
                array2--;
                Console.Write("Оператор --: ");
                array2.Print();

                OneDimensionArray array3 = array; //.Copy();
                array3 *= 3; 
                Console.Write("Оператор *= 3:");
                array3.Print();

                int[] intArr = array;

                array.Scalar = 2;
                Console.Write("Умноженный на скаляр 2 через свойство: ");
                array.Print();

                array = (OneDimensionArray)intArr;
                Console.WriteLine("Размерность массива: " + array.Size);                

                //array[1] = 777;
                //Console.WriteLine(array[1]);
                //Console.WriteLine(array[11]);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.ReadLine();
        }

        public static int ReadPositiveInt()
        {
            int x = 0;
            do
            {
                x = Convert.ToInt32(Console.ReadLine());
            } while (x < 1);
            return x;
        }
    }
}
