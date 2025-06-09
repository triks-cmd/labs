namespace MagazineApp
{
    public class Subscriber : IMagazineObserver
    {
        private const string ExcludedStreet = "Sovetskaya";

        public string LastName { get; }
        public string Address { get; }
        public string[] MagazineNames { get; private set; }

        private readonly decimal[] _prices;

        /// <summary>
        /// Общая стоимость всех подписок.
        /// </summary>
        public decimal TotalCost
        {
            get
            {
                decimal sum = 0m;
                
                for (int i = 0; i < _prices.Length; i++)
                {
                    sum += _prices[i];
                }
                
                return sum;
            }
        }

        public Subscriber(string lastName, string address, string[] magazineNames)
        {
            if (lastName == null)
            {
                throw new ArgumentNullException(nameof(lastName));
            }

            if (magazineNames == null)
            {
                throw new ArgumentNullException(nameof(magazineNames));
            }

            LastName = lastName;
            Address = address ?? string.Empty;
            MagazineNames = magazineNames;
            _prices = new decimal[magazineNames.Length];
        }

        /// <summary>
        /// Устанавливает цену подписки по индексу.
        /// </summary>
        public void SetMagazinePrice(int idx, decimal price)
        {
            bool indexOutOfRange = idx < 0 || idx >= _prices.Length;

            if (indexOutOfRange)
            {
                throw new IndexOutOfRangeException();
            }

            _prices[idx] = price;
        }

        /// <summary>
        /// Обновляет название в своём списке при уведомлении.
        /// </summary>
        public void OnMagazineNameChanged(string oldName, string newName)
        {
            for (int i = 0; i < MagazineNames.Length; i++)
            {
                bool matches = MagazineNames[i] == oldName;

                if (matches)
                {
                    MagazineNames[i] = newName;
                }
            }
        }

        /// <summary>
        /// Обновляет цену, если подписчик не живёт на «Sovetskaya».
        /// </summary>
        public void OnMagazinePriceChanged(string magazineName, decimal newPrice)
        {
            bool excluded = Address.Contains(ExcludedStreet);

            if (excluded)
            {
                return;
            }

            for (int i = 0; i < MagazineNames.Length; i++)
            {
                bool matches = MagazineNames[i] == magazineName;

                if (matches)
                {
                    _prices[i] = newPrice;
                }
            }
        }

        /// <summary>
        /// Возвращает список имён через запятую.
        /// </summary>
        public string GetMagazinesString()
        {
            string result = string.Empty;
            
            for (int i = 0; i < MagazineNames.Length; i++)
            {
                string name = MagazineNames[i];

                if (i == 0)
                {
                    result = name;
                }
                else
                {
                    result = result + ", " + name;
                }
            }

            return result;
        }
    }
}
