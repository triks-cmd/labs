namespace CreditCardApp
{
    /// <summary>
    /// Provides the functionality to validate the properties of a credit card.
    /// </summary>
    public interface ICardValidator
    {
        /// <summary>
        /// Validates that the card number, holder name, and credit limit meet the required criteria.
        /// </summary>
        /// <param name="cardNumber">The credit card number.</param>
        /// <param name="holderName">The card owner’s name.</param>
        /// <param name="creditLimit">The allowed credit limit.</param>
        void Validate(string cardNumber, string holderName, decimal creditLimit);
    }
}