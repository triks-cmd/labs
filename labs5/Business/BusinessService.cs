using System;

namespace Business
{
    /// <summary>
    /// Сервис бизнес-логики, использующий зависимости через абстракции.
    /// </summary>
    public class BusinessService
    {
        private readonly IManager _manager;
        private readonly IDeveloper _developer;
        private readonly IIntern _intern;

        /// <summary>
        /// Инициализирует новый экземпляр сервиса.
        /// </summary>
        /// <param name="manager">Менеджер.</param>
        /// <param name="developer">Разработчик.</param>
        /// <param name="intern">Стажер.</param>
        public BusinessService(IManager manager, IDeveloper developer, IIntern intern)
        {
            _manager = manager;
            _developer = developer;
            _intern = intern;
        }

        /// <summary>
        /// Запускает выполнение бизнес-логики.
        /// </summary>
        public void Run()
        {
            Console.WriteLine($"Менеджер: Зарплата {_manager.Salary}");
            _manager.Manage();

            Console.WriteLine($"\nРазработчик: Зарплата {_developer.Salary}");
            _developer.Develop();

            Console.WriteLine($"\nСтажер: Зарплата {_intern.Salary}");
            _intern.Learn();
        }
    }
}
