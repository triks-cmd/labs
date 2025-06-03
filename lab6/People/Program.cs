using PeopleApp.Factory;
using PeopleApp.Models;

namespace PeopleApp
{
    /// <summary>
    /// Основной класс программы. Реализует консольное меню и логику.
    /// </summary>
    public class Program
    {
        private readonly List<Person> _people = new List<Person>();

        /// <summary>
        /// Запускает логику приложения (после Main).
        /// </summary>
        /// <param name="filePath">Путь к входному файлу.</param>
        public void Run(string filePath)
        {
            LoadData(filePath);
            ShowMenu();
        }

        /// <summary>
        /// Загружает данные из файла при старте.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        private void LoadData(string filePath)
        {
            var factory = new PersonFactory();
            List<Person> loaded = factory.LoadPeople(filePath);

            foreach (Person person in loaded)
            {
                _people.Add(person);
            }
        }

        /// <summary>
        /// Отображает консольное меню и обрабатывает выбор пользователя.
        /// </summary>
        private void ShowMenu()
        {
            bool exitRequested = false;

            while (!exitRequested)
            {
                Console.WriteLine();
                Console.WriteLine("=== Menu ===");
                Console.WriteLine("1. Show me all people");
                Console.WriteLine("2. Show students (sorted by last name)");
                Console.WriteLine("3. Show the failures at school");
                Console.WriteLine("4. Show students applying for an increased scholarship");
                Console.WriteLine("5. Exit");
                Console.Write("Select an item (1-5): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllPersons();
                        break;

                    case "2":
                        DisplaySortedLearners();
                        break;

                    case "3":
                        DisplayFailingSchoolchildren();
                        break;

                    case "4":
                        DisplayScholarshipStudents();
                        break;

                    case "5":
                        exitRequested = true;
                        break;

                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте ещё раз.");
                        break;
                }
            }
        }

        /// <summary>
        /// Выводит на экран всех людей. Школьников старше порога – жёлтым цветом.
        /// </summary>
        private void DisplayAllPersons()
        {
            Console.WriteLine("--- Все люди ---");

            foreach (Person person in _people)
            {
                int age = DateTime.Now.Year - person.BirthYear;

                if (person is Schoolchild && age > Constants.SchoolchildAgeThreshold)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(person.GetDisplayString());
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(person.GetDisplayString());
                }
            }
        }

        /// <summary>
        /// Выводит всех учащихся, отсортировав по фамилии.
        /// </summary>
        private void DisplaySortedLearners()
        {
            Console.WriteLine("--- Учащиеся, отсортированные по фамилии ---");

            var learners = new List<Learner>();

            foreach (Person person in _people)
            {
                if (person is Learner learner)
                {
                    learners.Add(learner);
                }
            }

            learners.Sort();

            foreach (Learner ln in learners)
            {
                Console.WriteLine(ln.GetDisplayString());
            }
        }

        /// <summary>
        /// Показывает школьников, у которых есть оценка = Constants.FailingGrade.
        /// Запрашивает название школы у пользователя.
        /// </summary>
        private void DisplayFailingSchoolchildren()
        {
            Console.Write("Введите название школы для поиска «двойки»: ");
            string targetSchool = Console.ReadLine();
            Console.WriteLine();

            var failingSchoolchildren = new List<Schoolchild>();

            foreach (Person person in _people)
            {
                if (person is Schoolchild child)
                {
                    // Фильтрация по нужной школе
                    if (!child.Institution.Equals(targetSchool, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    foreach (int grade in child.Grades)
                    {
                        if (grade == Constants.FailingGrade)
                        {
                            failingSchoolchildren.Add(child);
                            break;
                        }
                    }
                }
            }

            Console.WriteLine($"--- Двоечники школы «{targetSchool}» ---");
            foreach (Schoolchild child in failingSchoolchildren)
            {
                Console.WriteLine(child.GetDisplayString());
            }
            Console.WriteLine($"Всего двоечников: {failingSchoolchildren.Count}");
        }

        /// <summary>
        /// Показывает студентов, претендующих на повышенную стипендию (все оценки ≥ Constants.ScholarshipMinGrade).
        /// </summary>
        private void DisplayScholarshipStudents()
        {
            Console.WriteLine("--- Студенты, претендующие на повышенную стипендию ---");

            foreach (Person person in _people)
            {
                if (person is UniversityStudent uniStudent)
                {
                    bool eligible = uniStudent.IsEligibleForScholarship();

                    if (eligible)
                    {
                        Console.WriteLine(uniStudent.GetDisplayString());
                    }
                }
            }
        }

        /// <summary>
        /// Точка входа приложения.
        /// </summary>
        /// <param name="args">Аргументы командной строки (первый аргумент – путь к файлу, по умолчанию «data.txt»).</param>
        public static void Main(string[] args)
        {
            string filePath = args.Length > 0 ? args[0] : "data.txt";
            var app = new Program();
            app.Run(filePath);
        }
    }
}
