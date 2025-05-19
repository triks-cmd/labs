using System;

namespace Business
{
    /// <summary>
    /// Represents a developer who writes code.
    /// </summary>
    public class Developer : Employee, IDeveloper
    {
        /// <summary>
        /// Executes code development tasks by writing a message to the console.
        /// </summary>
        public void Develop()
        {
            Console.WriteLine("The developer writes code");
        }
    }
}
