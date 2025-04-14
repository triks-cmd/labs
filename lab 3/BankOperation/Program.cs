/// <summary>
/// Entry point to the FinanceApp application, demonstrating the execution of banking operations.
/// </summary>
using FinanceApp.Models;

namespace FinanceApp
{
   
    class Program
    {
       
        static void Main()
        {
            IBankAccount accountA = new BankAccount("A341", 1000);
            IBankAccount accountB = new BankAccount("B345", 325);

            var operations = new BankOperation[]
            {
                new Deposit(accountA, 200),
                new Withdrawal(accountB, 345),
                new Transfer(accountA, accountB, 341)
            };

            foreach (var operation in operations)
            {
                operation.Execute();
            }

            Console.WriteLine($"Final balance {accountA.AccountNumber}: {accountA.Balance:C}");
            Console.WriteLine($"Final balance {accountB.AccountNumber}: {accountB.Balance:C}");
        }
    }
}