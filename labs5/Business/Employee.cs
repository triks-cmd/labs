using System;

namespace Business
{
    /// <summary>
    /// Базовый абстрактный класс для всех сотрудников.
    /// </summary>
    public abstract class Employee : IEmployee
    {
        private int _salary;
        private int _projectCount;

        /// <summary>
        /// Получает или задает зарплату сотрудника.
        /// </summary>
        public int Salary
        {
            get => _salary;
            set => _salary = value;
        }

        /// <summary>
        /// Назначает количество проектов.
        /// </summary>
        /// <param name="count">Количество проектов.</param>
        protected void AssignProject(int count)
        {
            _projectCount = count;
        }
    }
}
