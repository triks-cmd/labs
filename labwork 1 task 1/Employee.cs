using System;

namespace EmployeeManagement
{
    public class Employee

    {
        public string Surname { get; }
        public string Department { get; }
        public double[] Salaries { get; }

        public Employee(string surname, string department, double[] salaries)
        {
            Surname = surname;
            Department = department;
            Salaries = salaries;
        }

        public double AverageSalary
        {
            get
            {
                if (Salaries == null || Salaries.Length == 0)
                    return 0;

                double sum = 0;
                foreach (var salary in Salaries)
                {
                    sum += salary;
                }
                return sum / Salaries.Length;
            }
        }

     
        public double MinimumSalary
        {
            get
            {
                if (Salaries == null || Salaries.Length == 0)
                    return 0;

                double min = Salaries[0];
                for (int i = 1; i < Salaries.Length; i++)
                {
                    if (Salaries[i] < min)
                        min = Salaries[i];
                }
                return min;
            }
        }

        public double this[int index]
        {
            get
            {
                if (index >= 0 && index < Salaries.Length)
                    return Salaries[index];
                throw new IndexOutOfRangeException("Salary index is out of range.");
            }
            set
            {
                if (index >= 0 && index < Salaries.Length)
                    Salaries[index] = value;
                else
                {
                    throw new IndexOutOfRangeException("Salary index is out of range.");
                }
            }
        }

        public void Print()
        {
            Console.WriteLine($"Surname: {Surname}, Department: {Department}, Average Salary: {AverageSalary:F2}");
        }
    }
}
