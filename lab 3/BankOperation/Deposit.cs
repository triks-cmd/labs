/// <summary>
/// A class representing a deposit transaction that credits funds to a bank account.
/// </summary>
namespace FinanceApp.Models
{
   
    public class Deposit : BankOperation
    {
        private readonly IBankAccount _account;
        private readonly decimal _amount;

       
        public Deposit(IBankAccount account, decimal amount)
        {
            _account = account;
            _amount = amount;
        }

        public override void Execute()
        {
            _account.Deposit(_amount);
        }
    }
}