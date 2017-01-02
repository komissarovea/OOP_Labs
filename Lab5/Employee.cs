using System;

namespace Lab5
{
    public struct Employee : IComparable
    {
        private string name;
        private int entryYear;
        private string position;
        private int salary;
        private int workExperience;

        public int Salary
        {
            get
            {
                return salary;
            }
        }

        public Employee(string name, int entryYear, string position, int salary, int workExperience)
        {
            this.name = name;
            this.entryYear = entryYear;
            this.position = position;
            this.salary = salary;
            this.workExperience = workExperience;
        }

        public int CompareTo(object obj)
        {
            Employee other = (Employee)obj;
            return this.workExperience.CompareTo(other.workExperience);
        }

        public override string ToString()
        {
            return String.Format("{0}, {1}, {2}, {3}, {4}", name, entryYear, position, salary, workExperience);
        }
    }
}
