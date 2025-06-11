namespace MagazineApp
{
    /// <summary>
    /// Класс подписчика, реализует IMagazineObserver.
    /// </summary>
    public class Subscriber : IMagazineObserver
    {
        private const string ExcludedStreet = "Sovetskaya";

        public string LastName { get; }
        public string Address  { get; }
        public string[] MagazineNames { get; private set; }

        private decimal[] _prices;

        /// <summary>
        /// Общая стоимость всех подписок.
        /// </summary>
        public decimal TotalCost => _prices.Sum();

        public Subscriber(string lastName, string address, string[] magazineNames)
        {
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Address = address ?? string.Empty;
            MagazineNames = magazineNames ?? throw new ArgumentNullException(nameof(magazineNames));
            _prices = new decimal[magazineNames.Length];
        }

        public void SetMagazinePrice(int idx, decimal price)
        {
            if (idx < 0 || idx >= _prices.Length)
                throw new IndexOutOfRangeException();
            _prices[idx] = price;
        }

        public void OnMagazineNameChanged(string oldName, string newName)
        {
            for (int i = 0; i < MagazineNames.Length; i++)
                if (MagazineNames[i] == oldName)
                    MagazineNames[i] = newName;
        }

        public void OnMagazinePriceChanged(string magazineName, decimal newPrice)
        {
            if (Address.Contains(ExcludedStreet)) return;

            for (int i = 0; i < MagazineNames.Length; i++)
                if (MagazineNames[i] == magazineName)
                    _prices[i] = newPrice;
        }

        public override string ToString()
        {
            var names = string.Join(", ", MagazineNames);
            return $"{LastName}\t{Address}\t{names}\t{TotalCost}";
        }
    }
}
