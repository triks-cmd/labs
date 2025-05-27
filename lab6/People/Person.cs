using System;

namespace PeopleApp.Models
{
    /// <summary>
    /// Represents a generic person with basic information.
    /// </summary>
    public abstract class Person
    {
        /// <summary>Last name of the person.</summary>
        public string LastName { get; set; }

        /// <summary>Year of birth.</summary>
        public int BirthYear { get; set; }

        /// <summary>Status (student, teacher, businessman, etc.).</summary>
        public string Status { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        protected Person(string lastName, int birthYear, string status)
        {
            LastName = lastName;
            BirthYear = birthYear;
            Status = status;
        }

        /// <summary>
        /// Gets additional information about the person.
        /// </summary>
        public abstract string GetAdditionalInfo();

        /// <summary>
        /// Returns a formatted display string with all details.
        /// </summary>
        public virtual string GetDisplayString()
        {
            return $"{LastName}\t{Status}\t{BirthYear}\t{GetAdditionalInfo()}";
        }
    }
}