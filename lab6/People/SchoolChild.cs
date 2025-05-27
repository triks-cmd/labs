using System;

namespace PeopleApp.Models
{
    /// <summary>
    /// Represents a schoolchild with specific class number.
    /// </summary>
    public class Schoolchild : Learner
    {
        /// <summary>Numeric class identifier.</summary>
        public int ClassNumber { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Schoolchild"/> class.
        /// </summary>
        public Schoolchild(
            string lastName,
            int birthYear,
            string status,
            string institution,
            int classNumber,
            int[] grades)
            : base(lastName, birthYear, status, institution, grades)
        {
            ClassNumber = classNumber;
        }

        /// <inheritdoc/>
        public override string GetDisplayString()
        {
            return $"{LastName}\t{Institution}\t{ClassNumber}\t{Status}\t{BirthYear}\t{GetAdditionalInfo()}";
        }
    }
}