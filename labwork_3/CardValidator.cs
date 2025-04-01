using System;

namespace CreditCardApp
{
    /// <summary>
    /// The default validator for credit card details.
    /// </summary>
    public class CardValidator : ICardValidator
    {
        public void Validate(string cardNumber, string holderName, decimal creditLimit)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length != 16 || !IsAllDigits(cardNumber))
            {
                throw new ArgumentException("The card number must consist of 16 digits.", nameof(cardNumber));
            }

            if (string.IsNullOrWhiteSpace(holderName))
            {
                throw new ArgumentException("Owner name cannot be empty.", nameof(holderName));
            }

            if (creditLimit < 0)
            {
                throw new ArgumentException("The credit limit cannot be negative.", nameof(creditLimit));
            }
        }

        private bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}