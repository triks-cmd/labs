using System;

namespace Business
{
    /// <summary>
    /// Defines basic properties and actions for an employee.
    /// </summary>
    public interface IEmployee
    {
        /// <summary>
        /// Gets or sets the salary of the employee.
        /// </summary>
        int Salary { get; set; }

        /// <summary>
        /// Assigns a number of projects to this employee (overwrites any previous count).
        /// </summary>
        /// <param name="count">The new total number of projects.</param>
        void AssignProject(int count);

        /// <summary>
        /// Retrieves the current project count.
        /// </summary>
        /// <returns>The number of currently assigned projects.</returns>
        int GetAssignedProjectCount();
    }

    /// <summary>
    /// Defines the behavior of a manager.
    /// </summary>
    public interface IManager : IEmployee
    {
        /// <summary>
        /// Performs management tasks.
        /// </summary>
        void Manage();
    }

    /// <summary>
    /// Defines the behavior of a developer.
    /// </summary>
    public interface IDeveloper : IEmployee
    {
        /// <summary>
        /// Performs code development tasks.
        /// </summary>
        void Develop();
    }

    /// <summary>
    /// Defines the behavior of an intern.
    /// </summary>
    public interface IIntern : IEmployee
    {
        /// <summary>
        /// Performs learning tasks.
        /// </summary>
        void Learn();
    }
}
