namespace PeopleApp.Models
{
    /// <summary>
    /// Базовый класс для учащихся (Learner).
    /// Реализует IComparable&lt;Learner&gt; для сортировки по фамилии.
    /// </summary>
    public abstract class Learner : Person, IComparable<Learner>
    {
        /// <summary>Название учебного заведения.</summary>
        public string Institution { get; set; }

        /// <summary>Массив оценок.</summary>
        public int[] Grades { get; set; }

        /// <summary>
        /// Конструктор, инициализирующий фамилию, год рождения, статус, учреждение и оценки.
        /// </summary>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="birthYear">Год рождения.</param>
        /// <param name="status">Статус.</param>
        /// <param name="institution">Учебное заведение.</param>
        /// <param name="grades">Массив оценок.</param>
        protected Learner(
            string lastName,
            int birthYear,
            string status,
            string institution,
            int[] grades)
            : base(
                  lastName,
                  birthYear,
                  status)
        {
            Institution = institution;
            Grades = grades;
        }

        /// <summary>
        /// Вычисляет средний балл из всех оценок.
        /// </summary>
        /// <returns>Средний балл (double).</returns>
        public double CalculateAverageGrade()
        {
            if (Grades == null)
            {
                return 0.0;
            }

            int length = Grades.Length;

            if (length == 0)
            {
                return 0.0;
            }

            double sum = 0.0;

            foreach (int grade in Grades)
            {
                sum = sum + grade;
            }

            double average = sum / length;
            return average;
        }

        /// <inheritdoc/>
        public override string Info()
        {
            double avg = CalculateAverageGrade();
            string formatted = avg.ToString("0.0");
            return formatted;
        }

        /// <summary>
        /// Сравнивает двух Learner по фамилии (без учёта регистра).
        /// </summary>
        /// <param name="other">Другой Learner.</param>
        /// <returns>Результат сравнения фамилий.</returns>
        public int CompareTo(Learner other)
        {
            int comparison = string.Compare(
                LastName,
                other.LastName,
                StringComparison.OrdinalIgnoreCase);
            return comparison;
        }
    }
}
