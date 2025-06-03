namespace PeopleApp.Models
{
    /// <summary>
    /// Класс, представляющий студента университета (UniversityStudent).
    /// Наследует Learner, добавляет поле Group.
    /// </summary>
    public class UniversityStudent : Learner
    {
        /// <summary>Номер группы (строка).</summary>
        public string Group { get; set; }

        /// <summary>
        /// Конструктор, инициализирующий все поля UniversityStudent.
        /// </summary>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="birthYear">Год рождения.</param>
        /// <param name="status">Статус.</param>
        /// <param name="institution">Учебное заведение.</param>
        /// <param name="group">Номер группы.</param>
        /// <param name="grades">Массив оценок.</param>
        public UniversityStudent(
            string lastName,
            int birthYear,
            string status,
            string institution,
            string group,
            int[] grades)
            : base(
                  lastName,
                  birthYear,
                  status,
                  institution,
                  grades)
        {
            Group = group;
        }

        /// <summary>
        /// Проверяет, может ли студент получить повышенную стипендию 
        /// (все оценки ≥ Constants.ScholarshipMinGrade).
        /// </summary>
        /// <returns>True, если кандидатская.</returns>
        public bool IsEligibleForScholarship()
        {
            if (Grades == null)
            {
                return false;
            }

            int length = Grades.Length;

            if (length == 0)
            {
                return false;
            }

            foreach (int grade in Grades)
            {
                bool belowThreshold = grade < Constants.ScholarshipMinGrade;

                if (belowThreshold)
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Перегруженный вывод: 
        /// Фамилия ВУЗ Группа Статус Год Info().
        /// </remarks>
        public override string GetDisplayString()
        {
            string result = LastName;
            result = result + "\t";
            result = result + Institution;
            result = result + "\t";
            result = result + Group;
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
