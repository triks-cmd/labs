/// <summary>
/// A class that implements the operation of transferring funds from one bank account to another.
/// </summary>
namespace FinanceApp.Models
{
    /// <summary>
    /// Represents a transfer transaction that moves funds from a source bank account to a destination bank account.
    /// </summary>
    public class Transfer : BankOperation
    {
        private readonly IBankAccount _source;
        private readonly IBankAccount _destination;
        private readonly decimal _amount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Transfer"/> class with the specified source account, destination account, and transfer amount.
        /// </summary>
        /// <param name="source">The bank account from which funds will be withdrawn.</param>
        /// <param name="destination">The bank account to which funds will be deposited.</param>
        /// <param name="amount">The amount to be transferred.</param>
        public Transfer(IBankAccount source, IBankAccount destination, decimal amount)
        {
            _source = source;
            _destination = destination;
            _amount = amount;
        }

        /// <summary>
        /// Executes the transfer operation by withdrawing funds from the source account and depositing them into the destination account.
        /// </summary>
        public override void Execute()
        {
            if (_source.Withdraw(_amount))
            {
                _destination.Deposit(_amount);
                Console.WriteLine($"Transfer of {_amount:C} from {_source.AccountNumber} to {_destination.AccountNumber} completed");
            }
            else
            {
                Console.WriteLine($"Transfer failed from {_source.AccountNumber} to {_destination.AccountNumber}");
            }
        }
    }
}
