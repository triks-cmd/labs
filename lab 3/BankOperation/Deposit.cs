/// <summary>
/// A class representing a deposit transaction that credits funds to a bank account.
/// </summary>
namespace FinanceApp.Models
{
    /// <summary>
    /// Represents a deposit transaction that adds funds to the specified bank account.
    /// </summary>
    public class Deposit : BankOperation
    {
        private readonly IBankAccount _account;
        private readonly decimal _amount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Deposit"/> class with the specified bank account and deposit amount.
        /// </summary>
        /// <param name="account">The bank account to credit.</param>
        /// <param name="amount">The amount to deposit into the account.</param>
        public Deposit(IBankAccount account, decimal amount)
        {
            _account = account;
            _amount = amount;
        }

        /// <summary>
        /// Executes the deposit operation by adding funds to the bank account.
        /// </summary>
        public override void Execute()
        {
            _account.Deposit(_amount);
        }
    }
}
