namespace CreditCardApp
{
    /// <summary>
    /// The default implementation that formats a 16-digit card number.
    /// </summary>
    public class CardFormatter : ICardFormatter
    {
        public string Format(string cardNumber)
        {
            return string.Format("{0}-{1}-{2}-{3}",
                cardNumber.Substring(0, 4),
                cardNumber.Substring(4, 4),
                cardNumber.Substring(8, 4),
                cardNumber.Substring(12, 4));
        }
    }
}