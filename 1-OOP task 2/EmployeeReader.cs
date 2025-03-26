using System;

namespace EmployeeManagement
{
    public static class EmployeeReader
    {
        public static Employee[] ReadEmployees()
        {
            Console.Write("Please enter the number of employees: ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("Invalid number of employees.");
                return new Employee[0];
            }

            Employee[] employees = new Employee[n];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nEmployee {i + 1}:");

                Console.Write("Surname: ");
                string surname = Console.ReadLine();

                Console.Write("Department: ");
                string department = Console.ReadLine();

                Console.Write("Number of months for salary data: ");
                if (!int.TryParse(Console.ReadLine(), out int months) || months <= 0)
                {
                    Console.WriteLine("Invalid number of months. Skipping this employee.");
                    continue;
                }



                double[] salaries = new double[months];
                for (int j = 0; j < months; j++)
                {
                    Console.Write($"Salary for month {j + 1}: ");
                    if (!double.TryParse(Console.ReadLine(), out double salary))
                    {
                        Console.WriteLine("Invalid salary value. Using 0.");
                        salary = 0;
                    }

                    salaries[j] = salary;
                }


                employees[i] = new Employee(surname, department, salaries);
            }


            return employees;
        }
    }
}
