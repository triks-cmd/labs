using System;

namespace FinanceApp.Models
{
    public class Withdrawal : BankOperation
    {
        private readonly BankAccount _account;
        private readonly decimal _amount;

        public Withdrawal(BankAccount account, decimal amount)
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