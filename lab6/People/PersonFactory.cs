using PeopleApp.Models;

namespace PeopleApp.Factory
{
    /// <summary>
    /// Factory for creating <see cref="Person"/> objects from data files.
    /// </summary>
    public class PersonFactory
    {
        /// <summary>
        /// Loads people from a given text file path.
        /// </summary>
        public List<Person> LoadPeople(string filePath)
        {
            var people = new List<Person>();
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] tokens = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length < Constants.MinTokensForLearner)
                    continue;

                string lastName = tokens[Constants.LastNameIndex];
                if (!int.TryParse(tokens[Constants.BirthYearIndex], out int birthYear))
                    continue;
                string status = tokens[Constants.StatusIndex].ToLower();

                if (status == "student" || status == "ученик" || status == "школьник")
                {
                    if (tokens.Length < Constants.MinTokensForLearner)
                        continue;

                    string institution = tokens[Constants.InstitutionIndex];
                    string groupOrClass = tokens[Constants.GroupOrClassIndex];

                    var grades = new List<int>();
                    for (int i = Constants.GradesStartIndex; i < tokens.Length; i++)
                        if (int.TryParse(tokens[i], out int g))
                            grades.Add(g);

                    if (int.TryParse(groupOrClass, out int classNumber))
                    {
                        people.Add(new Schoolchild(lastName, birthYear, status, institution, classNumber, grades.ToArray()));
                    }
                    else
                    {
                        people.Add(new UniversityStudent(lastName, birthYear, status, institution, groupOrClass, grades.ToArray()));
                    }
                }
                else
                {
                    string workplace = tokens[Constants.WorkplaceIndex];
                    string position = string.Empty;
                    int salaryStart = Constants.SalariesStartIndex;

                    if (tokens.Length > Constants.PositionIndex && !int.TryParse(tokens[Constants.PositionIndex], out _))
                    {
                        position = tokens[Constants.PositionIndex];
                        salaryStart = Constants.PositionIndex + 1;
                    }

                    var salaries = new List<int>();
                    for (int i = salaryStart; i < tokens.Length; i++)
                        if (int.TryParse(tokens[i], out int sal))
                            salaries.Add(sal);

                    people.Add(new Worker(lastName, birthYear, status, workplace, position, salaries.ToArray()));
                }
            }

            return people;
        }
    }
}
