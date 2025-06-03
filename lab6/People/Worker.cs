namespace PeopleApp.Models
{
    /// <summary>
    /// Класс, представляющий рабочего (Worker).
    /// Наследует Person, добавляет Workplace, Position, Salaries.
    /// </summary>
    public class Worker : Person
    {
        /// <summary>Название рабочего места.</summary>
        public string Workplace { get; set; }

        /// <summary>Должность.</summary>
        public string Position { get; set; }

        /// <summary>Массив зарплат за 12 месяцев.</summary>
        public int[] Salaries { get; set; }

        /// <summary>
        /// Конструктор, инициализирующий все поля Worker.
        /// </summary>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="birthYear">Год рождения.</param>
        /// <param name="status">Статус.</param>
        /// <param name="workplace">Место работы.</param>
        /// <param name="position">Должность.</param>
        /// <param name="salaries">Массив зарплат.</param>
        public Worker(
            string lastName,
            int birthYear,
            string status,
            string workplace,
            string position,
            int[] salaries)
            : base(
                  lastName,
                  birthYear,
                  status)
        {
            Workplace = workplace;
            Position = position;
            Salaries = salaries;
        }

        /// <summary>Возвращает максимальную зарплату за все месяцы.</summary>
        /// <returns>Максимальная зарплата.</returns>
        public int GetMaxSalary()
        {
            if (Salaries == null)
            {
                return 0;
            }

            int length = Salaries.Length;

            if (length == 0)
            {
                return 0;
            }

            int max = Salaries[0];

            foreach (int salary in Salaries)
            {
                bool isGreater = salary > max;

                if (isGreater)
                {
                    max = salary;
                }
            }

            return max;
        }

        /// <inheritdoc/>
        public override string Info()
        {
            int maxSalary = GetMaxSalary();
            string asString = maxSalary.ToString();
            return asString;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Вывод: Фамилия Статус Год MaxSalary.
        /// </remarks>
        public override string GetDisplayString()
        {
            string result = LastName;
            result = result + "\t";
            result = result + Status;
            result = result + "\t";
            result = result + BirthYear;
            result = result + "\t";
            result = result + Info();
            return result;
        }
    }
}
