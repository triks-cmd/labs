using System;

namespace PeopleApp.Models
{
    /// <summary>
    /// Base class for learners (students or schoolchildren), supports sorting.
    /// </summary>
    public abstract class Learner : Person, IComparable<Learner>
    {
        /// <summary>Name of the educational institution.</summary>
        public string Institution { get; set; }

        /// <summary>Array of grades.</summary>
        public int[] Grades { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Learner"/> class.
        /// </summary>
        protected Learner(string lastName, int birthYear, string status, string institution, int[] grades)
            : base(lastName, birthYear, status)
        {
            Institution = institution;
            Grades = grades;
        }

        /// <summary>
        /// Calculates the average grade among all grades.
        /// </summary>
        public double CalculateAverageGrade()
        {
            if (Grades == null || Grades.Length == 0)
                return 0;
            double sum = 0;
            foreach (int grade in Grades)
                sum += grade;
            return sum / Grades.Length;
        }

        /// <inheritdoc/>
        public override string GetAdditionalInfo()
        {
            return CalculateAverageGrade().ToString("0.0");
        }

        /// <summary>
        /// Compares learners by last name for sorting.
        /// </summary>
        public int CompareTo(Learner other)
        {
            return string.Compare(LastName, other.LastName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
