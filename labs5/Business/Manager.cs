using System;

namespace Business
{
    /// <summary>
    /// Представляет менеджера, управляющего процессами.
    /// </summary>
    public class Manager : Employee, IManager
    {
        public void Manage()
        {
            Console.WriteLine("Менеджер управляет процессами");
        }
    }
}