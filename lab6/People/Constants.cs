namespace PeopleApp
{
    /// <summary>
    /// Общие константы приложения (никаких «магических чисел/строк» в коде).
    /// Класс НЕ объявлен как static, хотя константы внутри – const.
    /// </summary>
    public class Constants
    {
        /// <summary>Порог возраста для выделения школьников цветом.</summary>
        public const int SchoolchildAgeThreshold = 12;

        /// <summary>Оценка, считающаяся «неудовлетворительной».</summary>
        public const int FailingGrade = 2;

        /// <summary>Минимальное количество токенов в строке при разборе учащегося.</summary>
        public const int MinTokensForLearner = 6;

        /// <summary>Минимальная оценка для получения повышенной стипендии.</summary>
        public const int ScholarshipMinGrade = 5;

        /// <summary>Индексы токенов при разбиении строки.</summary>
        public const int LastNameIndex = 0;
        public const int BirthYearIndex = 1;
        public const int StatusIndex = 2;
        public const int InstitutionIndex = 3;
        public const int GroupOrClassIndex = 4;
        public const int GradesStartIndex = 5;

        /// <summary>Индексы токенов при разборе рабочего (Worker).</summary>
        public const int WorkplaceIndex = 3;
        public const int PositionIndex = 4;
        public const int SalariesStartIndex = 5;

        /// <summary>Строковые константы для статусов.</summary>
        public const string StatusStudent = "student";
        public const string StatusPupilRu = "ученик";
        public const string StatusSchoolboyRu = "школьник";
    }
}
