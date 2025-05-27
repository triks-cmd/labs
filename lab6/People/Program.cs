using PeopleApp.Factory;
using PeopleApp.Models;

namespace PeopleApp
{
    /// <summary>
    /// Main program workflow.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Runs the application logic.
        /// </summary>
        public void Run(string filePath)
        {
            PersonFactory factory = new PersonFactory();
            List<Person> people = factory.LoadPeople(filePath);

            Console.WriteLine("\n--- All Persons ---");
            foreach (Person person in people)
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
            Console.WriteLine();

            var learners = new List<Learner>();
            foreach (Person person in people)
                if (person is Learner learner)
                    learners.Add(learner);

            learners.Sort();
            Console.WriteLine("--- Learners Sorted by Last Name ---");
            foreach (Learner ln in learners)
                Console.WriteLine(ln.GetDisplayString());
            Console.WriteLine();

            var failingSchoolchildren = new List<Schoolchild>();
            foreach (Person person in people)
            {
                if (person is Schoolchild child)
                {
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

            Console.WriteLine("--- Failing Schoolchildren ---");
            foreach (Schoolchild child in failingSchoolchildren)
                Console.WriteLine(child.GetDisplayString());
            Console.WriteLine($"Total failing schoolchildren: {failingSchoolchildren.Count}\n");

            Console.WriteLine("--- University Students Eligible for Enhanced Scholarship ---");
            foreach (Person person in people)
            {
                if (person is UniversityStudent uniStudent && uniStudent.IsEligibleForScholarship())
                {
                    Console.WriteLine(uniStudent.GetDisplayString());
                }
            }
        }

        /// <summary>
        /// Application entry point.
        /// </summary>
        public static void Main(string[] args)
        {
            string filePath = args.Length > 0 ? args[0] : "data.txt";
            new Program().Run(filePath);
        }
    }
}