using System;

namespace Business
{
    /// <summary>
    /// Base abstract class for all employees, providing a salary property and project assignment.
    /// </summary>
    public abstract class Employee : IEmployee
    {
        /// <summary>
        /// Gets or sets the salary of the employee.
        /// </summary>
        public int Salary { get; set; }

        private int _projectCount;

        /// <inheritdoc />
        public void AssignProject(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Number of projects cannot be negative.");
            }
                
            _projectCount = count;
        }

        /// <inheritdoc />
        public int GetAssignedProjectCount()
        {
            return _projectCount;
        }
    }
}
