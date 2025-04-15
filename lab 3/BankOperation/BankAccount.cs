/// <summary>
/// Contains the IBankAccount interface and its BankAccount implementation for managing account balances and transactions.
/// </summary>
using System;

namespace FinanceApp.Models
{
    /// <summary>
    /// Defines the operations for a bank account.
    /// </summary>
    public interface IBankAccount
    {
        /// <summary>
        /// Gets the account number.
        /// </summary>
        string AccountNumber { get; }

        /// <summary>
        /// Gets the current balance of the account.
        /// </summary>
        decimal Balance { get; }

        /// <summary>
        /// Deposits the specified amount into the account.
        /// </summary>
        /// <param name="amount">The amount to deposit. Must be positive.</param>
        /// <exception cref="ArgumentException">Thrown when the deposit <paramref name="amount"/> is not positive.</exception>
        void Deposit(decimal amount);

        /// <summary>
        /// Withdraws the specified amount from the account.
        /// </summary>
        /// <param name="amount">The amount to withdraw. Must be positive.</param>
        /// <returns>
        /// <c>true</c> if the withdrawal was successful; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the withdrawal <paramref name="amount"/> is not positive.</exception>
        bool Withdraw(decimal amount);
    }

    /// <summary>
    /// Represents a bank account that supports deposits and withdrawals.
    /// </summary>
    public class BankAccount : IBankAccount
    {
        /// <summary>
        /// Gets the account number.
        /// </summary>
        public string AccountNumber { get; }

        /// <summary>
        /// Gets the current balance of the account.
        /// </summary>
        public decimal Balance { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccount"/> class with a specific account number and initial balance.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="initialBalance">The initial balance of the account.</param>
        public BankAccount(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        /// <summary>
        /// Deposits the specified amount into the account.
        /// </summary>
        /// <param name="amount">The amount to deposit. Must be positive.</param>
        /// <exception cref="ArgumentException">Thrown when the deposit <paramref name="amount"/> is not positive.</exception>
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive", nameof(amount));

            Balance += amount;
            Console.WriteLine($"{amount:C} deposited to {AccountNumber}. New balance: {Balance:C}");
        }

        /// <summary>
        /// Withdraws the specified amount from the account if sufficient funds exist.
        /// </summary>
        /// <param name="amount">The amount to withdraw. Must be positive.</param>
        /// <returns>
        /// <c>true</c> if the withdrawal was successful; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the withdrawal <paramref name="amount"/> is not positive.</exception>
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
