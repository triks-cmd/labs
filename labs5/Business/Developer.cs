using System;

namespace Business
{
    /// <summary>
    /// Представляет разработчика, пишущего код.
    /// </summary>
    public class Developer : Employee, IDeveloper
    {
        public void Develop()
        {
            Console.WriteLine("Разработчик пишет код");
          
        }
    }
}
