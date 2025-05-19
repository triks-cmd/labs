using System;

namespace Business
{
    /// <summary>
    /// Service that contains business logic and uses dependencies via abstractions.
    /// </summary>
    public class BusinessService
    {
        private readonly IManager _manager;
        private readonly IDeveloper _developer;
        private readonly IIntern _intern;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessService"/> class.
        /// </summary>
        /// <param name="manager">An implementation of <see cref="IManager"/>.</param>
        /// <param name="developer">An implementation of <see cref="IDeveloper"/>.</param>
        /// <param name="intern">An implementation of <see cref="IIntern"/>.</param>
        public BusinessService(IManager manager, IDeveloper developer, IIntern intern)
        {
            _manager = manager;
            _developer = developer;
            _intern = intern;
        }

        /// <summary>
        /// Executes the business logic by displaying salaries and invoking each role's behavior.
        /// </summary>
        public void Run()
        {
            Console.WriteLine($"Manager: Salary {_manager.Salary}");
            _manager.Manage();

            Console.WriteLine();

            Console.WriteLine($"Developer: Salary {_developer.Salary}");
            _developer.Develop();

            Console.WriteLine();

            Console.WriteLine($"Intern: Salary {_intern.Salary}");
            _intern.Learn();
        }
    }
}
