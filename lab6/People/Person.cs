namespace PeopleApp.Models
{
    /// <summary>
    /// Абстрактный базовый класс для любого человека.
    /// Содержит фамилию, год рождения и статус.
    /// </summary>
    public abstract class Person
    {
        /// <summary>Фамилия человека.</summary>
        public string LastName { get; set; }

        /// <summary>Год рождения человека.</summary>
        public int BirthYear { get; set; }

        /// <summary>Статус (student, teacher, businessman и т.д.).</summary>
        public string Status { get; set; }

        /// <summary>
        /// Конструктор, инициализирующий фамилию, год рождения и статус.
        /// </summary>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="birthYear">Год рождения.</param>
        /// <param name="status">Статус.</param>
        protected Person(
            string lastName,
            int birthYear,
            string status)
        {
            LastName = lastName;
            BirthYear = birthYear;
            Status = status;
        }

        /// <summary>Абстрактный метод для получения дополнительных сведений (Info).</summary>
        /// <returns>Строка с дополнительными сведениями о человеке.</returns>
        public abstract string Info();

        /// <summary>
        /// Формирует строку для вывода всех основных полей + дополнительных сведений.
        /// В виде: «Фамилия STATUS ГодРождения Info()».
        /// </summary>
        /// <returns>Сформированная строка.</returns>
        public virtual string GetDisplayString()
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
