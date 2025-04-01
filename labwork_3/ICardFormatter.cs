namespace CreditCardApp
{
    /// <summary>
    /// Provides the functionality to format a credit card number.
    /// </summary>
    public interface ICardFormatter
    {
        /// <summary>
        /// Formats a 16-digit card number.
        /// </summary>
        /// <param name="cardNumber">The unformatted card number.</param>
        /// <returns>The card number formatted (e.g. "1234-5678-1234-5678").</returns>
        string Format(string cardNumber);
    }
}