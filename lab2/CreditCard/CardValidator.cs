using System;

namespace CreditCardApp
{
    /// <summary>
    /// Provides functionality to validate credit card details.
    /// </summary>
    public class CardValidator : ICardValidator
    {
        private const int ValidCardLength = 16;

        
        public void Validate(string cardNumber, string holderName, decimal creditLimit)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                throw new ArgumentException("Card number cannot be empty.", nameof(cardNumber));
            }

            if (cardNumber.Length != ValidCardLength)
            {
                throw new ArgumentException(
                    $"Card number must have exactly {ValidCardLength} digits.",
                    nameof(cardNumber));
            }

            foreach (char character in cardNumber)
            {
                if (!char.IsDigit(character))
                {
                    throw new ArgumentException("Card number must contain only digits.", nameof(cardNumber));
                }
            }

            if (string.IsNullOrWhiteSpace(holderName))
            {
                throw new ArgumentException("Card holder's name cannot be empty.", nameof(holderName));
            }

            if (creditLimit < 0)
            {
                throw new ArgumentException("Credit limit cannot be negative.", nameof(creditLimit));
            }
        }
    }
}
