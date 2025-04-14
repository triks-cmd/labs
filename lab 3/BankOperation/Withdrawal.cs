/// <summary>
/// Class representing a bank account withdrawal operation.
/// </summary>
namespace FinanceApp.Models
{
    
    public class Withdrawal : BankOperation
    {
        private readonly IBankAccount _account;
        private readonly decimal _amount;

       
        public Withdrawal(IBankAccount account, decimal amount)
        {
            _account = account;
            _amount = amount;
        }

        public override void Execute()
        {
            _account.Withdraw(_amount);
        }
    }
}