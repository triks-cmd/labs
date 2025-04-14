/// <summary>
/// Contains the IBankAccount interface and its BankAccount implementation for managing account balances and transactions.
/// </summary>
using System;

namespace FinanceApp.Models
{
    
    public interface IBankAccount
    {
        
        string AccountNumber { get; }
        
        
        decimal Balance { get; }
        
       
        void Deposit(decimal amount);
        
        
        bool Withdraw(decimal amount);
    }

   
    public class BankAccount : IBankAccount
    {
        public string AccountNumber { get; }
        
        public decimal Balance { get; private set; }

        
        public BankAccount(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive", nameof(amount));

            Balance += amount;
            Console.WriteLine($"{amount:C} deposited to {AccountNumber}. New balance: {Balance:C}");
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive", nameof(amount));

            if (Balance < amount)
            {
                Console.WriteLine($"Insufficient funds in {AccountNumber}. Required: {amount:C}, Available: {Balance:C}");
                return false;
            }

            Balance -= amount;
            Console.WriteLine($"{amount:C} withdrawn from {AccountNumber}. Remaining: {Balance:C}");
            return true;
        }
    }
}