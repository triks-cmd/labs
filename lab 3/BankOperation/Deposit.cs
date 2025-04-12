using System;

namespace FinanceApp.Models
{
    public class Deposit : BankOperation
    {
        private readonly BankAccount _account;
        private readonly decimal _amount;

        public Deposit(BankAccount account, decimal amount)
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