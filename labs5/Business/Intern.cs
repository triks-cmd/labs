using System;

namespace Business
{
    /// <summary>
    /// Представляет стажера, который учится.
    /// </summary>
    public class Intern : Employee, IIntern
    {
        public void Learn()
        {
            Console.WriteLine("Стажер учится");
        }
    }
}
