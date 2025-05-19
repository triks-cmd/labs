using System;

namespace Business
{
    /// <summary>
    /// Represents an intern who is learning.
    /// </summary>
    public class Intern : Employee, IIntern
    {
        /// <summary>
        /// Executes learning tasks by writing a message to the console.
        /// </summary>
        public void Learn()
        {
            Console.WriteLine("Trainee studying");
        }
    }
}
