using System;
using Business;

namespace BusinessApp
{
    /// <summary>
    /// Точка входа в приложение.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            IManager manager = new Manager { Salary = 80000 };
            IDeveloper developer = new Developer { Salary = 60000 };
            IIntern intern = new Intern { Salary = 20000 };

            var businessService = new BusinessService(manager, developer, intern);
            businessService.Run();

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
