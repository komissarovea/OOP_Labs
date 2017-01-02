using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 5.	Создать класс для работы с одномерным массивом целых чисел. Разработать следующие элементы класса: 
//o	Поля: 
//	int [] IntArray; 
//	int n.
//o	Конструктор, позволяющий создать массив размерности n. 
//o	Методы, позволяющие: 
//	ввести элементы массива с клавиатуры; 
//	вывести элементы массива на экран; 
//	отсортировать элементы массива в порядке возрастания. 
//o	Свойства: 
//	возвращающее размерность массива (доступное только для чтения); 
//	позволяющее помножить все элементы массива на скаляр (доступное только для записи). 


namespace Lab2
{
    class OneDimensionArray
    {
        #region Fields

        private int[] IntArray;
        private int n;

        #endregion

        #region Properties

        public int Size
        {
            get { return IntArray.Length; }
        }

        public int Scalar
        {
            set
            {
                for (int i = 0; i < n; ++i)
                    IntArray[i] = IntArray[i] * value;
            }
        }

        public bool Sorted { get; private set; }

        public int this[int i]
        {
            get
            {
                if (i >= 0 && i < IntArray.Length)
                    return IntArray[i];
                else
                    throw new Exception("Ошибка! Выход за пределы массива!");
            }
            set
            {
                if (i >= 0 && i < IntArray.Length)
                    IntArray[i] = value;
                else
                    throw new Exception("Ошибка! Выход за пределы массива!");
            }
        }

        #endregion

        #region Constructor

        public OneDimensionArray(int size)
        {
            this.n = size;
            this.IntArray = new int[size];
        }

        #endregion

        #region Operators

        public static OneDimensionArray operator ++(OneDimensionArray arr)
        {
            int n = arr.Size;
            OneDimensionArray retval = new OneDimensionArray(n);
            for (int i = 0; i < n; ++i)
                retval[i] = arr[i] + 1;
            return retval;
        }

        public static OneDimensionArray operator --(OneDimensionArray arr)
        {
            int n = arr.Size;
            OneDimensionArray retval = new OneDimensionArray(n);
            for (int i = 0; i < n; ++i)
                retval[i] = arr[i] - 1;
            return retval;
        }

        public static bool operator !(OneDimensionArray arr)
        {
            return !arr.Sorted;
        }

        public static OneDimensionArray operator *(OneDimensionArray arr, int scalar)
        {
            int n = arr.Size;
            OneDimensionArray retval = new OneDimensionArray(n);
            for (int i = 0; i < n; ++i)
                retval[i] = arr[i] * scalar;
            return retval;
        }

        public static explicit operator OneDimensionArray(int[] arr)
        {
            int n = arr.Length;
            OneDimensionArray retval = new OneDimensionArray(n);
            for (int i = 0; i < n; ++i)
                retval[i] = arr[i];
            return retval;
        }

        //public static implicit operator OneDimensionArray(int[] arr)
        //{
        //    int n = arr.Length;
        //    OneDimensionArray retval = new OneDimensionArray(n);
        //    for (int i = 0; i < n; ++i)
        //        retval[i] = arr[i];
        //    return retval;
        //}

        public static implicit operator int[](OneDimensionArray arr)
        {
            int n = arr.Size;
            int[] retval = new int[n];
            for (int i = 0; i < n; ++i)
                retval[i] = arr[i];
            return retval;
        }

        #endregion

        #region Methods

        public void Input()
        {
            for (int i = 0; i < n; ++i)
            {
                Console.Write("[{0}] = ", i);
                IntArray[i] = ReadInt();
            }
        }

        public void Print()
        {
            if (IntArray.Length == 0)
                Console.WriteLine(" - ");

            foreach (int x in IntArray)
                Console.Write("{0} ", x);

            Console.WriteLine();
        }

        public OneDimensionArray Copy()
        {
            int n = this.Size;
            OneDimensionArray retval = new OneDimensionArray(n);
            for (int i = 0; i < n; ++i)
                retval[i] = this[i];
            return retval;
        }

        public void SortAsc()
        {
            int size = this.Size;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size - 1; j++)
                {
                    if (IntArray[j + 1] < IntArray[j])
                    {
                        int temp = IntArray[j];
                        IntArray[j] = IntArray[j + 1];
                        IntArray[j + 1] = temp;
                    }

                }
            }
            this.Sorted = true;
        }

        private int ReadInt()
        {
            int x = 0;
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out x))
                    break;
            }
            return x;
        }

        #endregion
    }
}
