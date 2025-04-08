using System;

namespace CreditCardApp
{
    /// <summary>
    /// Provides functionality to format a credit card number into groups.
    /// </summary>
    public class CardFormatter : ICardFormatter
    {
        private const int GroupSize = 4;

        public string Format(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
            {
                throw new ArgumentException("Card number cannot be null or empty.", nameof(cardNumber));
            }

            if (cardNumber.Length % GroupSize != 0)
            {
                throw new ArgumentException("Invalid card number length.", nameof(cardNumber));
            }

            string formattedNumber = string.Empty;
            for (int i = 0; i < cardNumber.Length; i++)
            {
                formattedNumber += cardNumber[i];

                if ((i + 1) % GroupSize == 0 && i != cardNumber.Length - 1)
                {
                    formattedNumber += "-";
                }
            }

            return formattedNumber;
        }
    }
}
