/// <summary>
/// Abstract base class for all bank operations that require execution.
/// </summary>
namespace FinanceApp.Models
{
    /// <summary>
    /// Provides an abstract base for executing bank operations.
    /// </summary>
    public abstract class BankOperation
    {
        /// <summary>
        /// Executes the bank operation.
        /// </summary>
        public abstract void Execute();
    }
}
