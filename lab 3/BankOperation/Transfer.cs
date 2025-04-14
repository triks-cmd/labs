/// <summary>
/// A class that implements the operation of transferring funds from one bank account to another.
/// </summary>
namespace FinanceApp.Models
{
    
    public class Transfer : BankOperation
    {
        private readonly IBankAccount _source;
        private readonly IBankAccount _destination;
        private readonly decimal _amount;

        
        public Transfer(IBankAccount source, IBankAccount destination, decimal amount)
        {
            _source = source;
            _destination = destination;
            _amount = amount;
        }

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