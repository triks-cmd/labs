using System;
using System.Linq;

namespace EmployeeManagement
{
    public static class EmployeeOperations
    {
        public static void SortEmployeesByDepartment(Employee[] employees)
        {
            Array.Sort(employees, (a, b) => a.Department.CompareTo(b.Department));
        }

        public static double ComputeOverallAverageSalary(Employee[] employees)
        {
            if (employees == null || employees.Length == 0)
                return 0;

            double total = 0;
            int count = 0;
            foreach (var emp in employees)
            {
                total += emp.AverageSalary;
                count++;
            }
            return total / count;
        }

        public static void PrintEmployeesAboveAverage(Employee[] employees, double overallAverage)
        {
            var grouped = employees.GroupBy(emp => emp.Department);
            foreach (var group in grouped)
            {
                int count = group.Count(emp => emp.AverageSalary > overallAverage);
                Console.WriteLine($"{group.Key}: {count} employee(s) with above average salary.");
            }
        }

        public static void PrintEmployeesWithMinimumSalaryBelow(Employee[] employees, double threshold)
        {
            bool found = false;
            foreach (var emp in employees)
            {
                if (emp.MinimumSalary < threshold)
                {
                    emp.Print();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No employees found with minimum salary below the given threshold.");
            }
        }
    }
}
