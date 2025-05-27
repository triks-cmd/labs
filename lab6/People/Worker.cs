using System;

namespace PeopleApp.Models
{
    /// <summary>
    /// Represents a working person with salary records.
    /// </summary>
    public class Worker : Person
    {
        /// <summary>Place of work.</summary>
        public string Workplace { get; set; }

        /// <summary>Position title.</summary>
        public string Position { get; set; }

        /// <summary>Salaries for each month.</summary>
        public int[] Salaries { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Worker"/> class.
        /// </summary>
        public Worker(
            string lastName,
            int birthYear,
            string status,
            string workplace,
            string position,
            int[] salaries)
            : base(lastName, birthYear, status)
        {
            Workplace = workplace;
            Position = position;
            Salaries = salaries;
        }

        /// <summary>
        /// Gets the maximum salary among all recorded months.
        /// </summary>
        public int GetMaxSalary()
        {
            if (Salaries == null || Salaries.Length == 0)
                return 0;
            int max = Salaries[0];
            foreach (int salary in Salaries)
                if (salary > max)
                    max = salary;
            return max;
        }

        /// <inheritdoc/>
        public override string GetAdditionalInfo()
        {
            return GetMaxSalary().ToString();
        }

        /// <inheritdoc/>
        public override string GetDisplayString()
        {
            return $"{LastName}\t{Status}\t{BirthYear}\t{GetAdditionalInfo()}";
        }
    }
}
