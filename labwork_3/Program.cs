using System;

namespace CreditCardApp
{
    class Program
    {
        static void Main()
        {
            ICardValidator validator = new CardValidator();
            ICardFormatter formatter = new CardFormatter();

            try
            {
                CreditCard card = new CreditCard("1234567812345678", "KIK BUTOWSKI", 500m, validator, formatter);

                card.Deposit(200m);
                bool withdrawn = card.Withdraw(100m);

                Console.WriteLine(card.GetCardInfo());
                Console.WriteLine($"Withdrawal successful: {withdrawn}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}