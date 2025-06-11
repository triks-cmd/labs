namespace MagazineApp
{
    /// <summary>
    /// Представляет журнал с возможностью подписки и уведомления подписчиков об изменениях.
    /// </summary>
    public class Magazine
    {
        private const string Err_InvalidNameOrEmpty = "Dont true name.";
        private const string Err_NegativePrice      = "Price dont minus.";

        private readonly List<IMagazineObserver> _observers;

        /// <summary>
        /// Получает текущее имя журнала.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Получает текущую годовую цену подписки.
        /// </summary>
        public decimal AnnualPrice { get; private set; }

        public Magazine(string name, decimal annualPrice)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(Err_InvalidNameOrEmpty, nameof(name));
            if (annualPrice < 0)
                throw new ArgumentException(Err_NegativePrice, nameof(annualPrice));

            Name = name;
            AnnualPrice = annualPrice;
            _observers = new List<IMagazineObserver>();
        }

        public void ChangeName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName) || newName == Name)
                throw new ArgumentException(Err_InvalidNameOrEmpty);

            var oldName = Name;
            Name = newName;
            NotifyNameChanged(oldName, newName);
        }

        public void ChangePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException(Err_NegativePrice);
            if (newPrice == AnnualPrice)
                return;

            AnnualPrice = newPrice;
            NotifyPriceChanged();
        }

        public void Subscribe(IMagazineObserver observer)
        {
            if (observer == null || _observers.Contains(observer)) return;
            _observers.Add(observer);
        }

        public void Unsubscribe(IMagazineObserver observer)
        {
            if (observer == null) return;
            _observers.Remove(observer);
        }

        private void NotifyNameChanged(string oldName, string newName)
        {
            foreach (var obs in _observers)
                obs.OnMagazineNameChanged(oldName, newName);
        }

        private void NotifyPriceChanged()
        {
            foreach (var obs in _observers)
                obs.OnMagazinePriceChanged(Name, AnnualPrice);
        }
    }
}
