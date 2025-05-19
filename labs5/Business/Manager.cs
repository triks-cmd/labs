using System;

namespace Business
{
    /// <summary>
    /// Represents a manager responsible for overseeing processes.
    /// </summary>
    public class Manager : Employee, IManager
    {
        /// <summary>
        /// Executes management tasks by writing a message to the console.
        /// </summary>
        public void Manage()
        {
            Console.WriteLine("Process Management Manager");
        }
    }
}
