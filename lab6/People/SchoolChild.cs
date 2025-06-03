namespace PeopleApp.Models
{
    /// <summary>
    /// Класс, представляющий школьника (Schoolchild).
    /// Наследует Learner, добавляет поле ClassNumber.
    /// </summary>
    public class Schoolchild : Learner
    {
        /// <summary>Номер класса.</summary>
        public int ClassNumber { get; set; }

        /// <summary>
        /// Конструктор, инициализирующий все поля Schoolchild.
        /// </summary>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="birthYear">Год рождения.</param>
        /// <param name="status">Статус.</param>
        /// <param name="institution">Учебное заведение.</param>
        /// <param name="classNumber">Номер класса.</param>
        /// <param name="grades">Массив оценок.</param>
        public Schoolchild(
            string lastName,
            int birthYear,
            string status,
            string institution,
            int classNumber,
            int[] grades)
            : base(
                  lastName,
                  birthYear,
                  status,
                  institution,
                  grades)
        {
            ClassNumber = classNumber;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Перегруженный вывод: 
        /// Фамилия Школа Класс Статус Год Info().
        /// </remarks>
        public override string GetDisplayString()
        {
            string result = LastName;
            result = result + "\t";
            result = result + Institution;
            result = result + "\t";
            result = result + ClassNumber;
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
