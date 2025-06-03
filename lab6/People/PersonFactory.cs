using PeopleApp.Models;

namespace PeopleApp.Factory
{
    /// <summary>
    /// Фабрика для создания объектов Person из текстового файла.
    /// </summary>
    public class PersonFactory
    {
        /// <summary>
        /// Загружает всех людей из указанного текстового файла.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <returns>Список Person.</returns>
        public List<Person> LoadPeople(string filePath)
        {
            var people = new List<Person>();

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] tokens = line
                    .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (tokens.Length < Constants.MinTokensForLearner)
                {
                    continue;
                }

                string lastName = tokens[Constants.LastNameIndex];

                bool birthYearParsed = int.TryParse(
                    tokens[Constants.BirthYearIndex],
                    out int birthYear);

                if (!birthYearParsed)
                {
                    continue;
                }

                string status = tokens[Constants.StatusIndex]
                    .ToLowerInvariant();

                // Если это школьник или студент
                if (status.Equals(Constants.StatusStudent, StringComparison.OrdinalIgnoreCase)
                    || status.Equals(Constants.StatusPupilRu, StringComparison.OrdinalIgnoreCase)
                    || status.Equals(Constants.StatusSchoolboyRu, StringComparison.OrdinalIgnoreCase))
                {
                    string institution = tokens[Constants.InstitutionIndex];
                    string groupOrClass = tokens[Constants.GroupOrClassIndex];

                    var gradesList = new List<int>();

                    for (int i = Constants.GradesStartIndex; i < tokens.Length; i++)
                    {
                        bool gradeParsed = int.TryParse(tokens[i], out int g);

                        if (gradeParsed)
                        {
                            gradesList.Add(g);
                        }
                    }

                    bool classParsed = int.TryParse(groupOrClass, out int classNumber);

                    if (classParsed)
                    {
                        var schoolchild = new Schoolchild(
                            lastName,
                            birthYear,
                            status,
                            institution,
                            classNumber,
                            gradesList.ToArray());

                        people.Add(schoolchild);
                    }
                    else
                    {
                        var uniStudent = new UniversityStudent(
                            lastName,
                            birthYear,
                            status,
                            institution,
                            groupOrClass,
                            gradesList.ToArray());

                        people.Add(uniStudent);
                    }
                }
                else
                {
                    // Обработка рабочих и преподавателей
                    string workplace = tokens[Constants.WorkplaceIndex];
                    string position = string.Empty;
                    int salaryStart = Constants.SalariesStartIndex;

                    if (tokens.Length > Constants.PositionIndex)
                    {
                        bool positionIsNumber = int.TryParse(
                            tokens[Constants.PositionIndex],
                            out _);

                        if (!positionIsNumber)
                        {
                            position = tokens[Constants.PositionIndex];
                            salaryStart = Constants.PositionIndex + 1;
                        }
                    }

                    var salariesList = new List<int>();

                    for (int i = salaryStart; i < tokens.Length; i++)
                    {
                        bool salaryParsed = int.TryParse(tokens[i], out int sal);

                        if (salaryParsed)
                        {
                            salariesList.Add(sal);
                        }
                    }

                    var worker = new Worker(
                        lastName,
                        birthYear,
                        status,
                        workplace,
                        position,
                        salariesList.ToArray());

                    people.Add(worker);
                }
            }

            return people;
        }
    }
}
