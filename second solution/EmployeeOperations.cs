using System;
using System.Linq;

namespace EmployeeManagement
{
    public class EmployeeOperations
    {
        private Employee[] _employees;

        public EmployeeOperations(Employee[] employees)
        {
            _employees = employees ?? throw new ArgumentNullException(nameof(employees));
        }

        public void SortEmployeesByDepartment()
        {
            Array.Sort(_employees, (a, b) => a.Department.CompareTo(b.Department));
        }

        public double ComputeOverallAverageSalary()
        {
            if (_employees.Length == 0)
                return 0;

            return _employees.Average(emp => emp.AverageSalary);
        }

        public void PrintEmployeesAboveAverage(double overallAverage)
        {
            var grouped = _employees.GroupBy(emp => emp.Department);
            foreach (var group in grouped)
            {
                int count = group.Count(emp => emp.AverageSalary > overallAverage);
                Console.WriteLine($"{group.Key}: {count} employee(s) with above average salary.");
            }
        }

        public void PrintEmployeesWithMinimumSalaryBelow(double threshold)
        {
            bool found = false;
            foreach (var emp in _employees)
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
