using System;

namespace CreditCardApp
{
    /// <summary>
    /// Represents a credit card with deposit and withdrawal functionality.
    /// </summary>
    public class CreditCard
    {
        private readonly string _cardNumber;
        private readonly string _holderName;
        private decimal _balance;
        private readonly decimal _creditLimit;
        private readonly ICardFormatter _cardFormatter;

        public string CardNumber => _cardFormatter.Format(_cardNumber);
        public string HolderName => _holderName;
        public decimal Balance => _balance;
        public decimal CreditLimit => _creditLimit;

        /// <summary>
        /// Initializes a new instance of the CreditCard class.
        /// This constructor uses dependency injection for validation and formatting.
        /// </summary>
        /// <param name="cardNumber">A 16-digit card number.</param>
        /// <param name="holderName">The card holder's name.</param>
        /// <param name="creditLimit">The maximum amount that can be in debt (allowed overdraft).</param>
        /// <param name="cardValidator">An implementation of ICardValidator.</param>
        /// <param name="cardFormatter">An implementation of ICardFormatter.</param>
        public CreditCard(
            string cardNumber, 
            string holderName, 
            decimal creditLimit, 
            ICardValidator cardValidator, 
            ICardFormatter cardFormatter)
        {
            cardValidator.Validate(cardNumber, holderName, creditLimit);

            _cardNumber = cardNumber;
            _holderName = holderName;
            _creditLimit = creditLimit;
            _balance = 0m;
            _cardFormatter = cardFormatter;
        }

        /// <summary>
        /// Deposits a positive amount into the card account.
        /// </summary>
        /// <param name="amount">The deposit amount.</param>
        public void Deposit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("The deposit amount cannot be negative.", nameof(amount));
            }

            _balance += amount;
        }

        /// <summary>
        /// Withdraws a positive amount if doing so does not exceed the allowed overdraft.
        /// </summary>
        /// <param name="amount">The withdrawal amount.</param>
        /// <returns>True if the withdrawal is successful; otherwise, false.</returns>
        public bool Withdraw(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("The withdrawal amount cannot be negative.", nameof(amount));
            }

            if (_balance - amount < -_creditLimit)
            {
                return false;
            }

            _balance -= amount;
            return true;
        }

        /// <summary>
        /// Retrieves formatted information about the credit card.
        /// </summary>
        /// <returns>A string containing the card number, holder, balance, and credit limit.</returns>
        public string GetCardInfo()
        {
            return $"Card: {CardNumber}, Holder: {_holderName}, Balance: {_balance:F2}, Credit Limit: {_creditLimit:F2}";
        }
    }
}