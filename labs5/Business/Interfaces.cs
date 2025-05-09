using System;

namespace Business
{
    /// <summary>
    /// Определяет базовые свойства сотрудника.
    /// </summary>
    public interface IEmployee
    {
        /// <summary>
        /// Получает или задает зарплату сотрудника.
        /// </summary>
        int Salary { get; set; }
    }

    /// <summary>
    /// Определяет поведение менеджера.
    /// </summary>
    public interface IManager : IEmployee
    {
        /// <summary>
        /// Управление процессами.
        /// </summary>
        void Manage();
    }

    /// <summary>
    /// Определяет поведение разработчика.
    /// </summary>
    public interface IDeveloper : IEmployee
    {
        /// <summary>
        /// Разработка кода.
        /// </summary>
        void Develop();
    }

    /// <summary>
    /// Определяет поведение стажера.
    /// </summary>
    public interface IIntern : IEmployee
    {
        /// <summary>
        /// Обучение.
        /// </summary>
        void Learn();
    }
}
