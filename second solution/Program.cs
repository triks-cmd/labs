using System;

namespace EmployeeManagement
{
    class Program
    {
        static void Main()
        {
            try
            {
                Employee[] employees = EmployeeReader.ReadEmployees();
                if (employees.Length == 0)
                {
                    Console.WriteLine("No employees to process.");
                    return;
                }

                var employeeOps = new EmployeeOperations(employees);

                employeeOps.SortEmployeesByDepartment();
                Console.WriteLine("\nEmployees sorted by department:");
                foreach (var emp in employees)
                {
                    emp.Print();
                }

                double overallAverage = employeeOps.ComputeOverallAverageSalary();
                Console.WriteLine($"\nOverall Average Salary: {overallAverage:F2}");
                Console.WriteLine("\nEmployees with above average salary by department:");
                employeeOps.PrintEmployeesAboveAverage(overallAverage);

                Console.Write("\nEnter the minimum salary threshold: ");
                if (!double.TryParse(Console.ReadLine(), out double minSalaryThreshold))
                {
                    Console.WriteLine("Invalid salary threshold.");
                    return;
                }

                Console.WriteLine("\nEmployees with minimum salary below threshold:");
                employeeOps.PrintEmployeesWithMinimumSalaryBelow(minSalaryThreshold);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
