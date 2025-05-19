using System;
using Business;

namespace BusinessApp
{
    /// <summary>
    /// Entry point for the application.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Main method: creates employees, runs the business service, and waits for exit.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        private static void Main(string[] args)
        {
            IManager manager = new Manager { Salary = 80000 };
            IDeveloper developer = new Developer { Salary = 60000 };
            IIntern intern = new Intern { Salary = 20000 };

            manager.AssignProject(3);
            developer.AssignProject(5);
            intern.AssignProject(1);

            var businessService = new BusinessService(manager, developer, intern);
            businessService.Run();

            Console.WriteLine();
            Console.WriteLine("Please press a button to exit)");
            Console.ReadKey();
        }
    }
}
