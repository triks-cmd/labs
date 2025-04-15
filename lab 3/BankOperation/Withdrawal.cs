/// <summary>
/// Class representing a bank account withdrawal operation.
/// </summary>
namespace FinanceApp.Models
{
    /// <summary>
    /// Represents a withdrawal transaction that deducts funds from the specified bank account.
    /// </summary>
    public class Withdrawal : BankOperation
    {
        private readonly IBankAccount _account;
        private readonly decimal _amount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Withdrawal"/> class with the specified bank account and withdrawal amount.
        /// </summary>
        /// <param name="account">The bank account from which funds will be withdrawn.</param>
        /// <param name="amount">The amount to withdraw from the account.</param>
        public Withdrawal(IBankAccount account, decimal amount)
        {
            _account = account;
            _amount = amount;
        }

        /// <summary>
        /// Executes the withdrawal operation by deducting the specified amount from the bank account.
        /// </summary>
        public override void Execute()
        {
            _account.Withdraw(_amount);
        }
    }
}
