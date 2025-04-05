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

        /// <summary>
        /// Gets the formatted card number.
        /// </summary>
        public string CardNumber => _cardFormatter.Format(_cardNumber);

        /// <summary>
        /// Gets the card holder's name.
        /// </summary>
        public string HolderName => _holderName;

        /// <summary>
        /// Gets the current balance of the card.
        /// </summary>
        public decimal Balance => _balance;

        /// <summary>
        /// Gets the credit limit of the card.
        /// </summary>
        public decimal CreditLimit => _creditLimit;

        /// <summary>
        /// Initializes a new instance of the CreditCard class with dependency injection.
        /// </summary>
        /// <param name="cardNumber">A 16-digit card number.</param>
        /// <param name="holderName">The card holder's name.</param>
        /// <param name="creditLimit">The maximum allowed overdraft.</param>
        /// <param name="cardValidator">Card validator implementation.</param>
        /// <param name="cardFormatter">Card formatter implementation.</param>
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
        /// Initializes a new instance of the CreditCard class with default dependencies.
        /// </summary>
        /// <param name="cardNumber">A 16-digit card number.</param>
        /// <param name="holderName">The card holder's name.</param>
        /// <param name="creditLimit">The maximum allowed overdraft.</param>
        public CreditCard(string cardNumber, string holderName, decimal creditLimit)
            : this(cardNumber,
                holderName,
                creditLimit,
                new CardValidator(),
                new CardFormatter())
        {
        }

        /// <summary>
        /// Deposits a positive amount into the card account.
        /// </summary>
        /// <param name="amount">The deposit amount.</param>
        /// <exception cref="ArgumentException">Thrown when amount is negative.</exception>
        public void Deposit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("The deposit amount cannot be negative.", nameof(amount));
            }

            _balance += amount;
        }

        /// <summary>
        /// Withdraws a positive amount if sufficient funds are available.
        /// </summary>
        /// <param name="amount">The withdrawal amount.</param>
        /// <returns>True if withdrawal succeeded, false otherwise.</returns>
        /// <exception cref="ArgumentException">Thrown when amount is negative.</exception>
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
        /// <returns>Formatted card information string.</returns>
        public string GetCardInfo()
        {
            return $"Card: {CardNumber}, Holder: {_holderName}, Balance: {_balance:F2}, Credir limit: {_creditLimit:F2}";
        }
    }
}