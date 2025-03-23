using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_OOP_task_2
{
    internal class Class1
    {
        class OOP
{
    static void Main()
    {
        Console.Write("Please Enter Employee");
        int n = int.Parse(Console.ReadLine());
        Employee[] employees = new Employee[n];

        for(int i = 0; i < n; i++)
        {
            Console.WriteLine("Employee" + (i + 1));
            Console.Write("Surname");
            string surname = Console.ReadLine();
            Console.Write("Department");
            string department = Console.ReadLine();
            Console.Write("number of months");
            int months = int.Parse(Console.ReadLine());
            double[] salary = new double[months];
            for(int j = 0; j < months; j++)
            {
                Console.WriteLine("salary of months" + (j + 1));
                salary[j] = double.Parse(Console.ReadLine());
            }

            employees[i] = new Employee(surname, department, salary);
        }

        Array.Sort(employees, (a,b) => a.GetDepartment().CompareTo(b.GetDepartment()));
        Console.WriteLine("employee sorted in department");

        for(int i = 0;i < employees.Length; i++)
        {
            employees[i].Print();
        }

        double totalMoney = 0;
        for(int i = 0; i < employees.Length; i++)
        {
            totalMoney += employees[i].GetBigSalary();
            totalMoney/= employees.Length;
            Console.WriteLine("Average Money" + totalMoney);
        }

        Console.WriteLine("employes who have a big salary than the general");

        for(int i = 0; i < employees.Length; i++)
        {
            string department = employees[i].GetDepartment();
            int count = 0;

            for(int j = 0; j < employees.Length; j++)
            {
                if(employees[j].GetDepartment() == department && employees[j].GetBigSalary() > totalMoney)
                 count++;

                Console.WriteLine(department + count);
            }
        }

        Console.WriteLine("Enter the minimum salary");

        double min = double.Parse(Console.ReadLine());
        bool found = false;

        Console.WriteLine("Employee who have a minimum salary");
        for(int i = 0; i<employees.Length; i++)
        {
            if(employees[i].GetSmallSalary() < min)
            {
                Console.WriteLine("Surname" + employees[i].GetSurname() + "Department" + employees[i].GetDepartment() + "Minimum salary" + employees[i].GetSmallSalary);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("dont have employees");
        }

        
    }

}


    }
}
