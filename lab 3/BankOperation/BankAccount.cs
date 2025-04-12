using System;

namespace FinanceApp.Models
{
    public class BankAccount
    {
        public string AccountNumber {get;}
        public decimal Balance {get; private set; }

        public BankAccount(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("The deposit amount must be greater than 0", nameof(amount));
            }

            Balance += amount;
            Console.WriteLine($"{amount:$} has been credited to account {AccountNumber}. New balance: {Balance:$}.");
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("The write-off amount must be greater than 0", nameof(amount));
            }

            if (Balance < amount)
            {
                Console.WriteLine($"There are not enough funds in account {AccountNumber} to write off {amount:$}.");
                return false;
            }

            Balance -= amount;
            Console.WriteLine($"From account{AccountNumber} written off {amount:$}. Remainder: {Balance:$}.");
            return true;
        }
    }
}