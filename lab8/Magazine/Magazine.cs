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

        /// <summary>
        /// Создает новый экземпляр <see cref="Magazine"/>.
        /// </summary>
        /// <param name="name">Имя журнала.</param>
        /// <param name="annualPrice">Годовая цена подписки.</param>
        /// <exception cref="ArgumentNullException">Если <paramref name="name"/> равен null.</exception>
        public Magazine(string name, decimal annualPrice)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            AnnualPrice = annualPrice;
            _observers = new List<IMagazineObserver>();
        }

        /// <summary>
        /// Меняет имя журнала и уведомляет подписчиков, если новое имя допустимо.
        /// </summary>
        /// <param name="newName">Новое имя журнала.</param>
        /// <exception cref="ArgumentException">Если новое имя пустое или совпадает с текущим.</exception>
        public void ChangeName(string newName)
        {
            bool isNullOrEmpty = string.IsNullOrWhiteSpace(newName);
            bool isSameAsOld = newName == Name;

            if (isNullOrEmpty || isSameAsOld)
            {
                throw new ArgumentException(Err_InvalidNameOrEmpty);
            }

            string oldName = Name;
            Name = newName;
            NotifyNameChanged(oldName, newName);
        }

        /// <summary>
        /// Меняет цену подписки и уведомляет подписчиков, если цена изменилась.
        /// </summary>
        /// <param name="newPrice">Новая цена подписки.</param>
        /// <exception cref="ArgumentException">Если цена отрицательная.</exception>
        public void ChangePrice(decimal newPrice)
        {
            bool isNegative = newPrice < 0;

            if (isNegative)
            {
                throw new ArgumentException(Err_NegativePrice);
            }

            bool isSameAsOld = newPrice == AnnualPrice;

            if (isSameAsOld)
            {
                return;
            }

            AnnualPrice = newPrice;
            NotifyPriceChanged();
        }

        /// <summary>
        /// Подписывает наблюдателя на уведомления от журнала.
        /// </summary>
        /// <param name="observer">Объект, реализующий интерфейс <see cref="IMagazineObserver"/>.</param>
        public void Subscribe(IMagazineObserver observer)
        {
            bool isNullObserver = observer == null;
            bool alreadySubscribed = _observers.Contains(observer);

            if (isNullObserver || alreadySubscribed)
            {
                return;
            }

            _observers.Add(observer);
        }

        /// <summary>
        /// Отписывает наблюдателя от уведомлений.
        /// </summary>
        /// <param name="observer">Объект, реализующий интерфейс <see cref="IMagazineObserver"/>.</param>
        public void Unsubscribe(IMagazineObserver observer)
        {
            if (observer == null)
            {
                return;
            }

            _observers.Remove(observer);
        }

        /// <summary>
        /// Уведомляет всех подписчиков об изменении имени журнала.
        /// </summary>
        /// <param name="oldName">Старое имя журнала.</param>
        /// <param name="newName">Новое имя журнала.</param>
        private void NotifyNameChanged(string oldName, string newName)
        {
            foreach (IMagazineObserver observer in _observers)
            {
                observer.OnMagazineNameChanged(oldName, newName);
            }
        }

        /// <summary>
        /// Уведомляет всех подписчиков об изменении цены.
        /// </summary>
        private void NotifyPriceChanged()
        {
            foreach (IMagazineObserver observer in _observers)
            {
                observer.OnMagazinePriceChanged(Name, AnnualPrice);
            }
        }
    }
}
