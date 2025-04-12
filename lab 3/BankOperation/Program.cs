using System;
using FinanceApp.Models;

namespace FinanceApp
{
    class Program
    {
        static void Main()
        {
            var accountA = new BankAccount("A341", 1000);
            var accountB = new BankAccount("B1921", 325);

            BankOperation depositOperation = new Deposit(accountA, 200);
            BankOperation withdrawalOperation = new Withdrawal(accountB, 345);
            BankOperation transferOperation = new Transfer(accountA, accountB, 341);


            depositOperation.Execute();
            withdrawalOperation.Execute();
            transferOperation.Execute();



            Console.WriteLine($"Total balance of account {accountA.AccountNumber}: {accountA.Balance:$}");
            Console.WriteLine($"Total balance of account {accountB.AccountNumber}: {accountB.Balance:$}");
        }
    }
}