using System;

namespace PeopleApp.Models
{
    /// <summary>
    /// Represents a university student with a group.
    /// </summary>
    public class UniversityStudent : Learner
    {
        /// <summary>Group identifier.</summary>
        public string Group { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniversityStudent"/> class.
        /// </summary>
        public UniversityStudent(
            string lastName,
            int birthYear,
            string status,
            string institution,
            string group,
            int[] grades)
            : base(lastName, birthYear, status, institution, grades)
        {
            Group = group;
        }

        /// <summary>
        /// Checks if the student qualifies for an enhanced scholarship (all grades >= 5).
        /// </summary>
        public bool IsEligibleForScholarship()
        {
            foreach (int grade in Grades)
                if (grade < 5)
                    return false;
            return true;
        }

        /// <inheritdoc/>
        public override string GetDisplayString()
        {
            return $"{LastName}\t{Institution}\t{Group}\t{Status}\t{BirthYear}\t{GetAdditionalInfo()}";
        }
    }
}