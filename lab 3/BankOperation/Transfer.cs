using System;

namespace FinanceApp.Models
{
    public class Transfer : BankOperation
    {
        private readonly BankAccount _sourceAccount;
        private readonly BankAccount _destinationAccount;
        private readonly decimal _amount;

        public Transfer(BankAccount sourceAccount, BankAccount destinationAccount, decimal amount)
        {
            _sourceAccount = sourceAccount;
            _destinationAccount = destinationAccount;
            _amount = amount;
        }

        public override void Execute()
        {
            if (_sourceAccount.Withdraw(_amount))
            {
                _destinationAccount.Deposit(_amount);
                Console.WriteLine($"Transfer of {_amount:$} from account {_sourceAccount.AccountNumber} to account {_destinationAccount.AccountNumber} completed successfully");
            }

            else
            {
                Console.WriteLine($"Transfer failed. Not enough funds in account {_sourceAccount.AccountNumber}");
            }
        }
    }
}