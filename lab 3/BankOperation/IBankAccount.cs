/// <summary>
/// Defines the operations for a bank account.
/// </summary>
namespace FinanceApp.Models
{
    public interface IBankAccount
    {
        /// <summary>
        /// Gets the account number.
        /// </summary>
        string AccountNumber { get; }

        /// <summary>
        /// Gets the current balance of the account.
        /// </summary>
        decimal Balance { get; }

        /// <summary>
        /// Deposits the specified amount into the account.
        /// </summary>
        /// <param name="amount">The amount to deposit. Must be positive.</param>
        /// <exception cref="ArgumentException">Thrown when the deposit <paramref name="amount"/> is not positive.</exception>
        void Deposit(decimal amount);

        /// <summary>
        /// Withdraws the specified amount from the account.
        /// </summary>
        /// <param name="amount">The amount to withdraw. Must be positive.</param>
        /// <returns>
        /// <c>true</c> if the withdrawal was successful; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the withdrawal <paramref name="amount"/> is not positive.</exception>
        bool Withdraw(decimal amount);
    }
}
